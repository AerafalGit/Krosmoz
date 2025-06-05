// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using Krosmoz.Core.Network.Internal;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http.Features;

namespace Krosmoz.Core.Network.Transport;

/// <summary>
/// Represents a socket-based connection that implements <see cref="ConnectionContext"/> and <see cref="IConnectionInherentKeepAliveFeature"/>.
/// Provides functionality for managing duplex communication over a socket.
/// </summary>
internal sealed class SocketConnection : ConnectionContext, IConnectionInherentKeepAliveFeature
{
    private readonly Socket _socket;
    private readonly EndPoint _endpoint;
    private readonly SocketSender _sender;
    private readonly SocketReceiver _receiver;

    private IDuplexPipe? _application;
    private volatile bool _aborted;

    /// <summary>
    /// Gets or sets the unique identifier for the connection.
    /// </summary>
    public override string ConnectionId { get; set; }

    /// <summary>
    /// Gets or sets the duplex pipe used for transport communication.
    /// </summary>
    public override IDuplexPipe Transport { get; set; }

    /// <summary>
    /// Gets the collection of features associated with the connection.
    /// </summary>
    public override IFeatureCollection Features { get; }

    /// <summary>
    /// Gets or sets the collection of items associated with the connection.
    /// </summary>
    public override IDictionary<object, object?> Items { get; set; }

    /// <summary>
    /// Gets a value indicating whether the connection has inherent keep-alive functionality.
    /// </summary>
    public bool HasInherentKeepAlive =>
        true;

    /// <summary>
    /// Initializes a new instance of the <see cref="SocketConnection"/> class.
    /// </summary>
    /// <param name="endpoint">The endpoint to connect to.</param>
    public SocketConnection(EndPoint endpoint)
    {
        _socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        _endpoint = endpoint;

        _sender = new SocketSender(_socket, PipeScheduler.ThreadPool);
        _receiver = new SocketReceiver(_socket, PipeScheduler.ThreadPool);

        Features = new FeatureCollection();
        Items = new ConnectionItems();
        ConnectionId = Guid.NewGuid().ToString();
        Transport = null!;

        Features.Set<IConnectionInherentKeepAliveFeature>(this);
    }

    /// <summary>
    /// Disposes the connection asynchronously, completing the input and output pipes.
    /// </summary>
    public override async ValueTask DisposeAsync()
    {
        if (Transport is not null)
        {
            await Transport.Output.CompleteAsync().ConfigureAwait(false);
            await Transport.Input.CompleteAsync().ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Starts the connection asynchronously by connecting the socket and initializing transport pipes.
    /// </summary>
    /// <returns>A <see cref="ValueTask{ConnectionContext}"/> representing the asynchronous operation.</returns>
    public async ValueTask<ConnectionContext> StartAsync()
    {
        await _socket.ConnectAsync(_endpoint).ConfigureAwait(false);

        var pair = DuplexPipe.CreateConnectionPair(PipeOptions.Default, PipeOptions.Default);

        LocalEndPoint = _socket.LocalEndPoint;
        RemoteEndPoint = _socket.RemoteEndPoint;

        Transport = pair.Transport;
        _application = pair.Application;

        _ = ExecuteAsync();

        return this;
    }

    /// <summary>
    /// Executes the main loop for sending and receiving data asynchronously.
    /// </summary>
    private async Task ExecuteAsync()
    {
        Exception? sendError = null;

        try
        {
            var receiveTask = DoReceive();
            var sendTask = DoSend();

            if (await Task.WhenAny(receiveTask, sendTask).ConfigureAwait(false) == sendTask)
                _socket.Dispose();

            await receiveTask.ConfigureAwait(false);
            sendError = await sendTask.ConfigureAwait(false);

            _socket.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected exception in {nameof(SocketConnection)}.{nameof(StartAsync)}: " + ex);
        }
        finally
        {
            await _application!.Input.CompleteAsync(sendError).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Handles the receiving of data asynchronously.
    /// </summary>
    private async Task DoReceive()
    {
        Exception? error = null;

        try
        {
            await ProcessReceives().ConfigureAwait(false);
        }
        catch (SocketException ex) when (ex.SocketErrorCode == SocketError.ConnectionReset)
        {
            error = new ConnectionResetException(ex.Message, ex);
        }
        catch (SocketException ex) when (ex.SocketErrorCode is SocketError.OperationAborted or
                                             SocketError.ConnectionAborted or
                                             SocketError.Interrupted or
                                             SocketError.InvalidArgument)
        {
            if (!_aborted)
                error = new ConnectionAbortedException();
        }
        catch (ObjectDisposedException)
        {
            if (!_aborted)
                error = new ConnectionAbortedException();
        }
        catch (IOException e)
        {
            error = e;
        }
        catch (Exception e)
        {
            error = new IOException(e.Message, e);
        }
        finally
        {
            if (_aborted)
                error ??= new ConnectionAbortedException();

            await _application!.Output.CompleteAsync(error).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Processes the receiving of data in a loop until the connection is closed or canceled.
    /// </summary>
    private async Task ProcessReceives()
    {
        while (true)
        {
            var buffer = _application!.Output.GetMemory();

            var bytesReceived = await _receiver.ReceiveAsync(buffer);

            if (bytesReceived is 0)
                break;

            _application.Output.Advance(bytesReceived);

            if (await _application.Output.FlushAsync().ConfigureAwait(false) is { IsCompleted: true })
                break;
        }
    }

    /// <summary>
    /// Handles the sending of data asynchronously.
    /// </summary>
    /// <returns>An <see cref="Exception"/> if an error occurs during sending; otherwise, null.</returns>
    private async Task<Exception?> DoSend()
    {
        Exception? error = null;

        try
        {
            await ProcessSends().ConfigureAwait(false);
        }
        catch (SocketException ex) when (ex.SocketErrorCode is SocketError.OperationAborted)
        {
            error = null;
        }
        catch (ObjectDisposedException)
        {
            error = null;
        }
        catch (IOException e)
        {
            error = e;
        }
        catch (Exception e)
        {
            error = new IOException(e.Message, e);
        }
        finally
        {
            _aborted = true;
            _socket.Shutdown(SocketShutdown.Both);
        }

        return error;
    }

    /// <summary>
    /// Processes the sending of data in a loop until the connection is closed or canceled.
    /// </summary>
    private async Task ProcessSends()
    {
        while (true)
        {
            var result = await _application!.Input.ReadAsync().ConfigureAwait(false);

            var buffer = result.Buffer;

            if (result.IsCanceled)
                break;

            var end = buffer.End;
            var isCompleted = result.IsCompleted;

            if (!buffer.IsEmpty)
                await _sender.SendAsync(buffer);

            _application.Input.AdvanceTo(end);

            if (isCompleted)
                break;
        }
    }
}

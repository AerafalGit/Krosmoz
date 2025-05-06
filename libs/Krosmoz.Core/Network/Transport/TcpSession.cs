// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using Krosmoz.Core.Network.Dispatcher;
using Krosmoz.Core.Network.Framing;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Core.Network.Transport;

/// <summary>
/// Represents an abstract TCP session for handling network communication.
/// </summary>
/// <typeparam name="TMessage">The type of the network message to handle.</typeparam>
public abstract class TcpSession<TMessage> : IAsyncDisposable
    where TMessage : class
{
    private readonly CancellationTokenSource _cts;
    private readonly Socket _socket;
    private readonly NetworkStream _stream;
    private readonly MessageReader<TMessage> _reader;
    private readonly MessageWriter<TMessage> _writer;
    private readonly IMessageDispatcher<TMessage> _dispatcher;
    private readonly ILogger<TcpSession<TMessage>> _logger;

    private bool _disposed;
    private string? _sessionId;

    /// <summary>
    /// Gets a value indicating whether the session is connected.
    /// </summary>
    public bool IsConnected =>
        !_cts.IsCancellationRequested && _socket.Connected && !_disposed;

    /// <summary>
    /// Gets the unique session ID.
    /// </summary>
    public string SessionId =>
        _sessionId ??= Guid.CreateVersion7(DateTimeOffset.UtcNow).ToString("N");

    /// <summary>
    /// Gets a cancellation token that is triggered when the connection is closed.
    /// </summary>
    public CancellationToken ConnectionClosed =>
        _cts.Token;

    /// <summary>
    /// Gets the remote endpoint of the session.
    /// </summary>
    public IPEndPoint RemoteEndPoint =>
        (IPEndPoint)_socket.RemoteEndPoint!;

    /// <summary>
    /// Gets the remote IP address of the session.
    /// </summary>
    public IPAddress RemoteAddress =>
        RemoteEndPoint.Address;

    /// <summary>
    /// Gets the remote port of the session.
    /// </summary>
    public int RemotePort =>
        RemoteEndPoint.Port;

    /// <summary>
    /// Initializes a new instance of the <see cref="TcpSession{TMessage}"/> class.
    /// </summary>
    /// <param name="socket">The socket used for the session.</param>
    /// <param name="messageDecoder">The decoder for network messages.</param>
    /// <param name="messageEncoder">The encoder for network messages.</param>
    /// <param name="dispatcher">The dispatcher for handling messages.</param>
    /// <param name="logger">The logger for the session.</param>
    protected TcpSession(
        Socket socket,
        IMessageDecoder<TMessage> messageDecoder,
        IMessageEncoder<TMessage> messageEncoder,
        IMessageDispatcher<TMessage> dispatcher,
        ILogger<TcpSession<TMessage>> logger)
    {
        _cts = new CancellationTokenSource();
        _dispatcher = dispatcher;
        _logger = logger;
        _socket = socket;
        _dispatcher = dispatcher;
        _logger = logger;
        _stream = new NetworkStream(socket, true);
        _reader = new MessageReader<TMessage>(PipeReader.Create(_stream), messageDecoder);
        _writer = new MessageWriter<TMessage>(PipeWriter.Create(_stream), messageEncoder);
    }

    /// <summary>
    /// Listens for incoming messages and processes them asynchronously.
    /// </summary>
    internal async Task ListenAsync()
    {
        _logger.LogInformation("Session {SessionName} connected", this);

        try
        {
            while (IsConnected)
            {
                try
                {
                    var result = await _reader.ReadAsync(_cts.Token).ConfigureAwait(false);

                    if (result.IsCanceled)
                        break;

                    var message = result.Message;

                    if (message is null)
                        break;

                    _logger.LogDebug("Session {SessionName} received message {Message}", this, GetMessageName(message));

                    await _dispatcher.DispatchMessageAsync(this, message).ConfigureAwait(false);
                }
                finally
                {
                    _reader.Advance();
                }
            }
        }
        catch (OperationCanceledException)
        {
            // ignore
        }
        catch (ObjectDisposedException)
        {
            // ignore
        }
        catch (Exception)
        {
            // ignore
        }
        finally
        {
            await DisposeAsync().ConfigureAwait(false);
        }

        _logger.LogInformation("Session {SessionName} disconnected", this);
    }

    /// <summary>
    /// Sends a message asynchronously.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendAsync(TMessage message)
    {
        return IsConnected ? _writer.WriteAsync(message, _cts.Token) : ValueTask.CompletedTask;
    }

    /// <summary>
    /// Disconnects the session asynchronously.
    /// </summary>
    public async Task DisconnectAsync()
    {
        if (_cts.IsCancellationRequested)
            return;

        await _cts.CancelAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Disconnects the session asynchronously after a specified delay.
    /// </summary>
    /// <param name="delay">The delay before disconnecting.</param>
    public async Task DisconnectAsync(TimeSpan delay)
    {
        if (_cts.IsCancellationRequested)
            return;

        await Task.Delay(delay, _cts.Token).ConfigureAwait(false);

        await _cts.CancelAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Disposes the session asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        if (_disposed)
            return;

        _disposed = true;

        if (!_cts.IsCancellationRequested)
            await _cts.CancelAsync().ConfigureAwait(false);

        await _reader.DisposeAsync().ConfigureAwait(false);
        await _writer.DisposeAsync().ConfigureAwait(false);
        await _stream.DisposeAsync().ConfigureAwait(false);

        _cts.Dispose();

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Returns a string representation of the session.
    /// </summary>
    /// <returns>The session ID as a string.</returns>
    public override string ToString()
    {
        return SessionId;
    }

    /// <summary>
    /// Gets the name of the message for logging or processing purposes.
    /// </summary>
    /// <param name="message">The message to get the name for.</param>
    /// <returns>The name of the message.</returns>
    protected abstract string GetMessageName(TMessage message);
}

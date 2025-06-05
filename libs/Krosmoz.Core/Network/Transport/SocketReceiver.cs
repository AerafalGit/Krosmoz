// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.IO.Pipelines;
using System.Net.Sockets;

namespace Krosmoz.Core.Network.Transport;

/// <summary>
/// Represents a receiver for socket data.
/// Provides functionality to asynchronously receive data from a socket using a custom scheduler.
/// </summary>
internal sealed class SocketReceiver
{
    private readonly Socket _socket;
    private readonly SocketAsyncEventArgs _eventArgs;
    private readonly SocketAwaitable _awaitable;

    /// <summary>
    /// Initializes a new instance of the <see cref="SocketReceiver"/> class.
    /// </summary>
    /// <param name="socket">The socket to receive data from.</param>
    /// <param name="scheduler">The scheduler used for I/O operations.</param>
    public SocketReceiver(Socket socket, PipeScheduler scheduler)
    {
        _socket = socket;
        _awaitable = new SocketAwaitable(scheduler);
        _eventArgs = new SocketAsyncEventArgs();
        _eventArgs.UserToken = _awaitable;
        _eventArgs.Completed += static (_, e) => ((SocketAwaitable)e.UserToken!).Complete(e.BytesTransferred, e.SocketError);
    }

    /// <summary>
    /// Receives data asynchronously from the socket into the specified buffer.
    /// </summary>
    /// <param name="buffer">The memory buffer to store the received data.</param>
    /// <returns>A <see cref="SocketAwaitable"/> representing the asynchronous operation.</returns>
    public SocketAwaitable ReceiveAsync(Memory<byte> buffer)
    {
        _eventArgs.SetBuffer(buffer);

        if (!_socket.ReceiveAsync(_eventArgs))
            _awaitable.Complete(_eventArgs.BytesTransferred, _eventArgs.SocketError);

        return _awaitable;
    }
}

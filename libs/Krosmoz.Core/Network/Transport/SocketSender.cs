// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Krosmoz.Core.Extensions;

namespace Krosmoz.Core.Network.Transport;

/// <summary>
/// Represents a sender for socket data.
/// Provides functionality to asynchronously send data over a socket using a custom scheduler.
/// </summary>
internal sealed class SocketSender
{
    private readonly Socket _socket;
    private readonly SocketAsyncEventArgs _eventArgs;
    private readonly SocketAwaitable _awaitable;

    /// <summary>
    /// A reusable list of array segments for sending data.
    /// </summary>
    private List<ArraySegment<byte>>? _bufferList;

    /// <summary>
    /// Initializes a new instance of the <see cref="SocketSender"/> class.
    /// </summary>
    /// <param name="socket">The socket to send data through.</param>
    /// <param name="scheduler">The scheduler used for I/O operations.</param>
    public SocketSender(Socket socket, PipeScheduler scheduler)
    {
        _socket = socket;
        _awaitable = new SocketAwaitable(scheduler);
        _eventArgs = new SocketAsyncEventArgs();
        _eventArgs.UserToken = _awaitable;
        _eventArgs.Completed += static (_, e) => ((SocketAwaitable)e.UserToken!).Complete(e.BytesTransferred, e.SocketError);
    }

    /// <summary>
    /// Sends data asynchronously over the socket using a sequence of buffers.
    /// </summary>
    /// <param name="buffers">The sequence of buffers containing the data to send.</param>
    /// <returns>A <see cref="SocketAwaitable"/> representing the asynchronous operation.</returns>
    public SocketAwaitable SendAsync(in ReadOnlySequence<byte> buffers)
    {
        if (buffers.IsSingleSegment)
            return SendAsync(buffers.First);

        if (!_eventArgs.MemoryBuffer.Equals(Memory<byte>.Empty))
            _eventArgs.SetBuffer(null, 0, 0);

        _eventArgs.BufferList = GetBufferList(buffers);

        if (!_socket.SendAsync(_eventArgs))
        {
            _awaitable.Complete(_eventArgs.BytesTransferred, _eventArgs.SocketError);
        }

        return _awaitable;
    }

    /// <summary>
    /// Sends data asynchronously over the socket using a single memory buffer.
    /// </summary>
    /// <param name="memory">The memory buffer containing the data to send.</param>
    /// <returns>A <see cref="SocketAwaitable"/> representing the asynchronous operation.</returns>
    private SocketAwaitable SendAsync(ReadOnlyMemory<byte> memory)
    {
        if (_eventArgs.BufferList is not null)
            _eventArgs.BufferList = null;

        _eventArgs.SetBuffer(MemoryMarshal.AsMemory(memory));

        if (!_socket.SendAsync(_eventArgs))
            _awaitable.Complete(_eventArgs.BytesTransferred, _eventArgs.SocketError);

        return _awaitable;
    }

    /// <summary>
    /// Converts a sequence of buffers into a list of array segments for sending data.
    /// </summary>
    /// <param name="buffer">The sequence of buffers to convert.</param>
    /// <returns>A list of <see cref="ArraySegment{T}"/> representing the buffers.</returns>
    private List<ArraySegment<byte>> GetBufferList(in ReadOnlySequence<byte> buffer)
    {
        Debug.Assert(!buffer.IsEmpty);
        Debug.Assert(!buffer.IsSingleSegment);

        if (_bufferList is null)
            _bufferList = [];
        else
            _bufferList.Clear();

        foreach (var segment in buffer)
            _bufferList.Add(segment.GetArray());

        return _bufferList;
    }
}

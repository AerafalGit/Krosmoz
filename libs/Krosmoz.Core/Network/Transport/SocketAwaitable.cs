// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using System.IO.Pipelines;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Krosmoz.Core.Network.Transport;

/// <summary>
/// Represents an awaitable object for socket operations.
/// Provides functionality to handle asynchronous socket operations with custom scheduling.
/// </summary>
internal sealed class SocketAwaitable : ICriticalNotifyCompletion
{
    private static readonly Action s_callbackCompleted = static () => { };

    private readonly PipeScheduler _ioScheduler;

    private Action? _callback;
    private int _bytesTransferred;
    private SocketError _error;

    /// <summary>
    /// Gets a value indicating whether the operation is completed.
    /// </summary>
    public bool IsCompleted =>
        ReferenceEquals(_callback, s_callbackCompleted);

    /// <summary>
    /// Initializes a new instance of the <see cref="SocketAwaitable"/> class.
    /// </summary>
    /// <param name="ioScheduler">The scheduler used for I/O operations.</param>
    public SocketAwaitable(PipeScheduler ioScheduler)
    {
        _ioScheduler = ioScheduler;
    }

    /// <summary>
    /// Gets the awaiter for this instance.
    /// </summary>
    /// <returns>The current instance of <see cref="SocketAwaitable"/>.</returns>
    public SocketAwaitable GetAwaiter()
    {
        return this;
    }

    /// <summary>
    /// Gets the result of the socket operation.
    /// </summary>
    /// <returns>The number of bytes transferred.</returns>
    /// <exception cref="SocketException">Thrown if the socket operation resulted in an error.</exception>
    public int GetResult()
    {
        Debug.Assert(ReferenceEquals(_callback, s_callbackCompleted));

        _callback = null;

        if (_error is not SocketError.Success)
            throw new SocketException((int)_error);

        return _bytesTransferred;
    }

    /// <summary>
    /// Schedules the specified continuation action to run when the operation is completed.
    /// </summary>
    /// <param name="continuation">The action to run upon completion.</param>
    public void OnCompleted(Action continuation)
    {
        if (ReferenceEquals(_callback, s_callbackCompleted) ||
            ReferenceEquals(Interlocked.CompareExchange(ref _callback, continuation, null), s_callbackCompleted))
        {
            Task.Run(continuation);
        }
    }

    /// <summary>
    /// Schedules the specified continuation action to run when the operation is completed without capturing the execution context.
    /// </summary>
    /// <param name="continuation">The action to run upon completion.</param>
    public void UnsafeOnCompleted(Action continuation)
    {
        OnCompleted(continuation);
    }

    /// <summary>
    /// Completes the socket operation and schedules the continuation action.
    /// </summary>
    /// <param name="bytesTransferred">The number of bytes transferred during the operation.</param>
    /// <param name="socketError">The error code resulting from the operation.</param>
    public void Complete(int bytesTransferred, SocketError socketError)
    {
        _error = socketError;
        _bytesTransferred = bytesTransferred;

        var continuation = Interlocked.Exchange(ref _callback, s_callbackCompleted);

        if (continuation is not null)
            _ioScheduler.Schedule(state => ((Action)state!)(), continuation);
    }
}

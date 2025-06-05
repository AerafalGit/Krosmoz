// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Krosmoz.Core.Collections;

/// <summary>
/// Represents a thread-safe queue that supports atomic operations for unmanaged numeric types.
/// </summary>
/// <typeparam name="T">The type of elements in the queue, which must be unmanaged and implement <see cref="INumber{TSelf}"/>.</typeparam>
public class AtomicQueue<T>
    where T : unmanaged, INumber<T>
{
    /// <summary>
    /// The underlying concurrent queue.
    /// </summary>
    protected readonly ConcurrentQueue<T> Queue;

    /// <summary>
    /// The highest ID in the queue.
    /// </summary>
    protected T HighestId;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtomicQueue{T}"/> class with an empty queue and the highest ID set to zero.
    /// </summary>
    public AtomicQueue()
    {
        Queue = [];
        HighestId = T.Zero;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AtomicQueue{T}"/> class with an empty queue and the specified highest ID.
    /// </summary>
    /// <param name="highestId">The initial highest ID.</param>
    public AtomicQueue(T highestId)
    {
        Queue = [];
        HighestId = highestId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AtomicQueue{T}"/> class with the specified collection of IDs.
    /// </summary>
    /// <param name="ids">The collection of IDs to initialize the queue with.</param>
    public AtomicQueue(IEnumerable<T> ids)
    {
        Queue = new ConcurrentQueue<T>();
        HighestId = T.Zero;

        foreach (var id in ids)
            Queue.Enqueue(id);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AtomicQueue{T}"/> class with the specified concurrent queue and highest ID.
    /// </summary>
    /// <param name="queue">The concurrent queue to initialize with.</param>
    /// <param name="highestId">The initial highest ID.</param>
    public AtomicQueue(ConcurrentQueue<T> queue, T highestId)
    {
        Queue = queue;
        HighestId = highestId;
    }

    /// <summary>
    /// Returns the element at the front of the queue without removing it.
    /// </summary>
    /// <returns>The element at the front of the queue, or the incremented highest ID if the queue is empty.</returns>
    public virtual T Peek()
    {
        if (!Queue.IsEmpty && Queue.TryPeek(out var id))
            return id;

        return Increment(ref HighestId);
    }

    /// <summary>
    /// Removes and returns the element at the front of the queue.
    /// </summary>
    /// <returns>The element at the front of the queue, or the incremented highest ID if the queue is empty.</returns>
    public virtual T Pop()
    {
        if (!Queue.IsEmpty && Queue.TryDequeue(out var id))
            return id;

        return Increment(ref HighestId);
    }

    /// <summary>
    /// Adds an element to the end of the queue.
    /// </summary>
    /// <param name="id">The element to add.</param>
    public virtual void Push(T id)
    {
        Queue.Enqueue(id);
    }

    /// <summary>
    /// Clears the queue and resets the highest ID to zero.
    /// </summary>
    public virtual void Reset()
    {
        Queue.Clear();
        HighestId = T.Zero;
    }

    /// <summary>
    /// Returns the next highest ID.
    /// </summary>
    /// <returns>The incremented highest ID.</returns>
    protected virtual T Next()
    {
        return Increment(ref HighestId);
    }

    /// <summary>
    /// Atomically increments the specified location and returns the new value.
    /// </summary>
    /// <param name="location">The location to increment.</param>
    /// <returns>The incremented value.</returns>
    protected static unsafe T Increment(ref T location)
    {
        switch (sizeof(T))
        {
            case sizeof(byte):
                Unsafe.BitCast<byte, T>(Interlocked.Exchange(ref Unsafe.As<T, byte>(ref location), Unsafe.BitCast<T, byte>(location + T.One)));
                break;

            case sizeof(short):
                Unsafe.BitCast<short, T>(Interlocked.Exchange(ref Unsafe.As<T, short>(ref location), Unsafe.BitCast<T, short>(location + T.One)));
                break;

            case sizeof(int):
                Unsafe.BitCast<int, T>(Interlocked.Exchange(ref Unsafe.As<T, int>(ref location), Unsafe.BitCast<T, int>(location + T.One)));
                break;

            case sizeof(long):
                Unsafe.BitCast<long, T>(Interlocked.Exchange(ref Unsafe.As<T, long>(ref location), Unsafe.BitCast<T, long>(location + T.One)));
                break;
        }

        return location;
    }

    /// <summary>
    /// Atomically decrements the specified location and returns the new value.
    /// </summary>
    /// <param name="location">The location to decrement.</param>
    /// <returns>The decremented value.</returns>
    protected static unsafe T Decrement(ref T location)
    {
        switch (sizeof(T))
        {
            case sizeof(byte):
                Unsafe.BitCast<byte, T>(Interlocked.Exchange(ref Unsafe.As<T, byte>(ref location), Unsafe.BitCast<T, byte>(location - T.One)));
                break;

            case sizeof(short):
                Unsafe.BitCast<short, T>(Interlocked.Exchange(ref Unsafe.As<T, short>(ref location), Unsafe.BitCast<T, short>(location - T.One)));
                break;

            case sizeof(int):
                Unsafe.BitCast<int, T>(Interlocked.Exchange(ref Unsafe.As<T, int>(ref location), Unsafe.BitCast<T, int>(location - T.One)));
                break;

            case sizeof(long):
                Unsafe.BitCast<long, T>(Interlocked.Exchange(ref Unsafe.As<T, long>(ref location), Unsafe.BitCast<T, long>(location - T.One)));
                break;
        }

        return location;
    }
}

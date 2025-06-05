// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Numerics;

namespace Krosmoz.Core.Collections;

/// <summary>
/// Represents a reverse atomic queue that supports atomic operations for unmanaged numeric types.
/// </summary>
/// <typeparam name="T">The type of elements in the queue, which must be unmanaged and implement <see cref="INumber{TSelf}"/>.</typeparam>
public sealed class ReverseAtomicQueue<T> : AtomicQueue<T>
    where T : unmanaged, INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReverseAtomicQueue{T}"/> class with the specified highest ID.
    /// </summary>
    /// <param name="highestId">The initial highest ID.</param>
    public ReverseAtomicQueue(T highestId) : base(highestId)
    {
    }

    /// <summary>
    /// Returns the next highest ID by decrementing the current highest ID.
    /// </summary>
    /// <returns>The decremented highest ID.</returns>
    protected override T Next()
    {
        return Decrement(ref HighestId);
    }

    /// <summary>
    /// Returns the element at the front of the queue without removing it, or the decremented highest ID if the queue is empty.
    /// </summary>
    /// <returns>The element at the front of the queue, or the decremented highest ID if the queue is empty.</returns>
    public override T Peek()
    {
        if (!Queue.IsEmpty && Queue.TryPeek(out var id))
            return id;

        return Decrement(ref HighestId);
    }
}

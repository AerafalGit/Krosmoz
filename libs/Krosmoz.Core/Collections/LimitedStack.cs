// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections;

namespace Krosmoz.Core.Collections;

/// <summary>
/// Represents a stack with a limited capacity.
/// </summary>
/// <typeparam name="T">The type of elements in the stack.</typeparam>
public sealed class LimitedStack<T> : IEnumerable<T>
{
    private readonly List<T> _items;
    private readonly int _capacity;

    /// <summary>
    /// Gets the number of elements contained in the stack.
    /// </summary>
    public int Count =>
        _items.Count;

    /// <summary>
    /// Gets a value indicating whether the stack is empty.
    /// </summary>
    public bool IsEmpty =>
        _items.Count is 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="LimitedStack{T}"/> class with the specified capacity.
    /// </summary>
    /// <param name="capacity">The maximum number of elements the stack can hold.</param>
    public LimitedStack(int capacity)
    {
        _items = new List<T>(capacity);
        _capacity = capacity;
    }

    /// <summary>
    /// Returns the element at the top of the stack without removing it.
    /// </summary>
    /// <returns>The element at the top of the stack.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("The stack is empty.");

        return _items[^1];
    }

    /// <summary>
    /// Removes and returns the element at the top of the stack.
    /// </summary>
    /// <returns>The element removed from the top of the stack.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    public T Pop()
    {
        if (IsEmpty)
            throw new InvalidOperationException("The stack is empty.");

        var item = _items[^1];
        _items.RemoveAt(_items.Count - 1);

        return item;
    }

    /// <summary>
    /// Adds an element to the top of the stack. If the stack is at capacity, the bottom element is removed.
    /// </summary>
    /// <param name="item">The element to add to the stack.</param>
    public void Push(T item)
    {
        if (_items.Count == _capacity)
            _items.RemoveAt(0);

        _items.Add(item);
    }

    /// <summary>
    /// Removes all elements from the stack.
    /// </summary>
    public void Clear()
    {
        _items.Clear();
    }

    /// <summary>
    /// Copies the elements of the stack to a new array.
    /// </summary>
    /// <returns>An array containing copies of the elements of the stack.</returns>
    public T[] ToArray()
    {
        return _items.ToArray();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the stack.
    /// </summary>
    /// <returns>An enumerator for the stack.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the stack.
    /// </summary>
    /// <returns>An enumerator for the stack.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

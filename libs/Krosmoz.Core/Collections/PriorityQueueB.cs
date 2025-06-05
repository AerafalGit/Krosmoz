// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections;

namespace Krosmoz.Core.Collections;

/// <summary>
/// Represents a priority queue with a binary heap implementation.
/// </summary>
/// <typeparam name="T">The type of elements in the queue.</typeparam>
public sealed class PriorityQueueB<T> : IEnumerable<T>
{
    private readonly List<T> _items;
    private readonly IComparer<T> _comparer;

    /// <summary>
    /// Gets the number of elements contained in the queue.
    /// </summary>
    public int Count =>
        _items.Count;

    /// <summary>
    /// Gets a value indicating whether the queue is empty.
    /// </summary>
    public bool IsEmpty =>
        _items.Count is 0;

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    public T this[int index]
    {
        get => _items[index];
        set
        {
            _items[index] = value;
            Update(index);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueueB{T}"/> class with the default comparer.
    /// </summary>
    public PriorityQueueB()
    {
        _items = [];
        _comparer = Comparer<T>.Default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueueB{T}"/> class with the specified comparer.
    /// </summary>
    /// <param name="comparer">The comparer to use for comparing elements.</param>
    public PriorityQueueB(IComparer<T> comparer)
    {
        _items = [];
        _comparer = comparer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueueB{T}"/> class with the specified comparer and initial capacity.
    /// </summary>
    /// <param name="comparer">The comparer to use for comparing elements.</param>
    /// <param name="capacity">The initial capacity of the queue.</param>
    public PriorityQueueB(IComparer<T> comparer, int capacity)
    {
        _items = new List<T>(capacity);
        _comparer = comparer;
    }

    /// <summary>
    /// Switches the elements at the specified indices.
    /// </summary>
    /// <param name="i">The index of the first element.</param>
    /// <param name="j">The index of the second element.</param>
    public void Switch(int i, int j)
    {
        (_items[i], _items[j]) = (_items[j], _items[i]);
    }

    /// <summary>
    /// Compares the elements at the specified indices.
    /// </summary>
    /// <param name="i">The index of the first element.</param>
    /// <param name="j">The index of the second element.</param>
    /// <returns>A signed integer that indicates the relative values of the elements.</returns>
    public int Compare(int i, int j)
    {
        return _comparer.Compare(_items[i], _items[j]);
    }

    /// <summary>
    /// Adds an element to the queue and returns its index.
    /// </summary>
    /// <param name="item">The element to add.</param>
    /// <returns>The index of the added element.</returns>
    public int Push(T item)
    {
        var count = _items.Count;

        _items.Add(item);

        do
        {
            if (count is 0)
                break;

            var count2 = (count - 1) / 2;

            if (Compare(count, count2) < 0)
            {
                Switch(count, count2);
                count = count2;
            }
            else
                break;
        } while (true);

        return count;
    }

    /// <summary>
    /// Returns the element at the front of the queue without removing it.
    /// </summary>
    /// <returns>The element at the front of the queue, or the default value if the queue is empty.</returns>
    public T? Peek()
    {
        return _items.Count is 0 ? default : _items[0];
    }

    /// <summary>
    /// Removes and returns the element at the front of the queue.
    /// </summary>
    /// <returns>The element removed from the front of the queue.</returns>
    public T Pop()
    {
        var result = _items[0];

        var p = 0;

        _items[0] = _items[^1];
        _items.RemoveAt(_items.Count - 1);

        do
        {
            var pn = p;
            var p1 = 2 * p + 1;
            var p2 = 2 * p + 2;

            if (_items.Count > p1 && Compare(p, p1) > 0)
                p = p1;

            if (_items.Count > p2 && Compare(p, p2) > 0)
                p = p2;

            if (p == pn)
                break;

            Switch(p, pn);
        } while (true);

        return result;
    }

    /// <summary>
    /// Removes the specified element from the queue.
    /// </summary>
    /// <param name="item">The element to remove.</param>
    public void Remove(T item)
    {
        var index = -1;

        for (var i = 0; i < _items.Count; i++)
        {
            if (_comparer.Compare(_items[i], item) is 0)
                index = i;
        }

        if (index is not -1)
            _items.RemoveAt(index);
    }

    /// <summary>
    /// Updates the position of the element at the specified index.
    /// </summary>
    /// <param name="i">The index of the element to update.</param>
    public void Update(int i)
    {
        var p = i;
        int p2;

        do
        {
            if (p is 0)
                break;

            p2 = (p - 1) / 2;

            if (Compare(p, p2) < 0)
            {
                Switch(p, p2);
                p = p2;
            }
            else
                break;
        } while (true);

        if (p < i)
            return;

        do
        {
            var pn = p;
            var p1 = 2 * p + 1;
            p2 = 2 * p + 2;

            if (_items.Count > p1 && Compare(p, p1) > 0)
                p = p1;

            if (_items.Count > p2 && Compare(p, p2) > 0)
                p = p2;

            if (p == pn)
                break;

            Switch(p, pn);
        } while (true);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the elements of the queue.
    /// </summary>
    /// <returns>An enumerator for the queue.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the elements of the queue.
    /// </summary>
    /// <returns>An enumerator for the queue.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

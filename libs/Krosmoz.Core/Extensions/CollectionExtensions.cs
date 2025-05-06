// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for converting IEnumerable collections to thread-safe collections
/// such as ConcurrentBag and ConcurrentDictionary.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Retrieves a random element from the source collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source collection.</typeparam>
    /// <param name="source">The source collection to retrieve a random element from.</param>
    /// <returns>A random element from the source collection.</returns>
    /// <exception cref="ArgumentException">Thrown if the source collection is empty.</exception>
    [Pure]
    public static T RandomElement<T>(this IEnumerable<T> source)
    {
        switch (source)
        {
            case T[] array:
                ThrowIfEmpty(array.Length, nameof(array));
                return array[Random.Shared.Next(array.Length)];

            case List<T> list:
                ThrowIfEmpty(list.Count, nameof(list));
                return list[Random.Shared.Next(list.Count)];

            case ICollection<T> collection:
                ThrowIfEmpty(collection.Count, nameof(collection));
                return collection.ElementAt(Random.Shared.Next(collection.Count));

            case IReadOnlyCollection<T> readOnlyCollection:
                ThrowIfEmpty(readOnlyCollection.Count, nameof(readOnlyCollection));
                return readOnlyCollection.ElementAt(Random.Shared.Next(readOnlyCollection.Count));

            default:
                var sourceAsArray = source.ToArray();
                ThrowIfEmpty(sourceAsArray.Length, nameof(sourceAsArray));
                return sourceAsArray[Random.Shared.Next(sourceAsArray.Length)];
        }

        [Pure]
        static void ThrowIfEmpty(int length, [CallerArgumentExpression(nameof(length))] string? paramName = null)
        {
            if (length is 0)
                throw new ArgumentException("The source collection is empty.", paramName);
        }
    }

    /// <summary>
    /// Retrieves a random element from the source collection that matches the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source collection.</typeparam>
    /// <param name="source">The source collection to retrieve a random element from.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>A random element from the source collection that matches the predicate.</returns>
    /// <exception cref="ArgumentException">Thrown if no elements match the predicate.</exception>
    [Pure]
    public static T RandomElement<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var filteredSource = source.Where(predicate).ToArray();

        if (filteredSource.Length is 0)
            throw new ArgumentException("The source collection is empty.", nameof(source));

        return filteredSource[Random.Shared.Next(filteredSource.Length)];
    }

    /// <summary>
    /// Retrieves a random element from the source collection or the default value if the collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source collection.</typeparam>
    /// <param name="source">The source collection to retrieve a random element from.</param>
    /// <returns>A random element from the source collection, or the default value if the collection is empty.</returns>
    [Pure]
    public static T? RandomElementOrDefault<T>(this IEnumerable<T> source)
    {
        switch (source)
        {
            case T[] array:
                return array.Length is 0 ? default : array[Random.Shared.Next(array.Length)];

            case List<T> list:
                return list.Count is 0 ? default : list[Random.Shared.Next(list.Count)];

            case ICollection<T> collection:
                return collection.Count is 0 ? default : collection.ElementAt(Random.Shared.Next(collection.Count));

            case IReadOnlyCollection<T> readOnlyCollection:
                return readOnlyCollection.Count is 0 ? default : readOnlyCollection.ElementAt(Random.Shared.Next(readOnlyCollection.Count));

            default:
                var sourceAsArray = source.ToArray();
                return sourceAsArray.Length is 0 ? default : sourceAsArray[Random.Shared.Next(sourceAsArray.Length)];
        }
    }

    /// <summary>
    /// Retrieves a random element from the source collection that matches the specified predicate,
    /// or the default value if no elements match the predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source collection.</typeparam>
    /// <param name="source">The source collection to retrieve a random element from.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>A random element from the source collection that matches the predicate, or the default value if no elements match.</returns>
    [Pure]
    public static T? RandomElementOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var filteredSource = source.Where(predicate).ToArray();

        return filteredSource.Length is 0
            ? default
            : filteredSource[Random.Shared.Next(filteredSource.Length)];
    }

    /// <summary>
    /// Compares two IEnumerable collections for equality, ignoring the order of elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="first">The first collection to compare.</param>
    /// <param name="second">The second collection to compare.</param>
    /// <returns>True if the collections contain the same elements, otherwise false.</returns>
    [Pure]
    public static bool CompareEnumerable<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        var firstArray = first.ToArray();
        var secondArray = second.ToArray();

        if (firstArray.Length != secondArray.Length)
            return false;

        return !firstArray.Except(secondArray).Any() && !secondArray.Except(firstArray).Any();
    }

    /// <summary>
    /// Converts an IEnumerable source to a ConcurrentBag.
    /// </summary>
    /// <typeparam name="TValue">The type of elements in the source.</typeparam>
    /// <param name="source">The IEnumerable source to convert.</param>
    /// <returns>A ConcurrentBag containing the elements of the source.</returns>
    [Pure]
    public static ConcurrentBag<TValue> ToConcurrentBag<TValue>(this IEnumerable<TValue> source)
    {
        return new ConcurrentBag<TValue>(source);
    }

    /// <summary>
    /// Converts an IEnumerable source to a ConcurrentDictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IEnumerable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <returns>A ConcurrentDictionary containing the elements of the source.</returns>
    [Pure]
    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector)
        where TKey : notnull
    {
        return new ConcurrentDictionary<TKey, TValue>(source.ToDictionary(keySelector));
    }

    /// <summary>
    /// Converts an IEnumerable source to a ConcurrentDictionary with a custom comparer.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IEnumerable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="comparer">An IEqualityComparer to compare keys.</param>
    /// <returns>A ConcurrentDictionary containing the elements of the source.</returns>
    [Pure]
    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
        where TKey : notnull
    {
        return new ConcurrentDictionary<TKey, TValue>(source.ToDictionary(keySelector, comparer), comparer);
    }

    /// <summary>
    /// Converts an IEnumerable source to a ConcurrentDictionary with a custom value selector.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IEnumerable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="valueSelector">A function to extract a value from each element.</param>
    /// <returns>A ConcurrentDictionary containing the elements of the source.</returns>
    [Pure]
    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector)
        where TKey : notnull
    {
        return new ConcurrentDictionary<TKey, TValue>(source.ToDictionary(keySelector, valueSelector));
    }

    /// <summary>
    /// Converts an IEnumerable source to a ConcurrentDictionary with a custom value selector and comparer.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IEnumerable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="valueSelector">A function to extract a value from each element.</param>
    /// <param name="comparer">An IEqualityComparer to compare keys.</param>
    /// <returns>A ConcurrentDictionary containing the elements of the source.</returns>
    [Pure]
    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector, IEqualityComparer<TKey> comparer)
        where TKey : notnull
    {
        return new ConcurrentDictionary<TKey, TValue>(source.ToDictionary(keySelector, valueSelector, comparer), comparer);
    }
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for converting IEnumerable collections to thread-safe collections
/// such as ConcurrentBag and ConcurrentDictionary.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Converts an IEnumerable source to a ConcurrentBag.
    /// </summary>
    /// <typeparam name="TValue">The type of elements in the source.</typeparam>
    /// <param name="source">The IEnumerable source to convert.</param>
    /// <returns>A ConcurrentBag containing the elements of the source.</returns>
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
    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector, IEqualityComparer<TKey> comparer)
        where TKey : notnull
    {
        return new ConcurrentDictionary<TKey, TValue>(source.ToDictionary(keySelector, valueSelector, comparer), comparer);
    }
}

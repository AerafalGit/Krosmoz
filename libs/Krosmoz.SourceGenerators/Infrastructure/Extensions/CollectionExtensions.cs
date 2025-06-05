// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.SourceGenerators.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for working with collections.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Returns a sequence of distinct elements from the source collection based on a specified key selector.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source collection.</typeparam>
    /// <typeparam name="TKey">The type of the key used for determining distinct elements.</typeparam>
    /// <param name="source">The source collection to filter for distinct elements.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> containing distinct elements from the source collection
    /// based on the specified key.
    /// </returns>
    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
    {
        var seenKeys = new HashSet<TKey>();

        foreach (var element in source)
        {
            if (seenKeys.Add(keySelector(element)))
                yield return element;
        }
    }

    /// <summary>
    /// Enumerates a collection and returns each element along with its index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to enumerate.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of tuples where each tuple contains the index
    /// and the corresponding element from the collection.
    /// </returns>
    public static IEnumerable<(int, T)> Index<T>(this IEnumerable<T> collection)
    {
        var index = -1;

        foreach (var element in collection)
        {
            index++;
            yield return (index, element);
        }
    }
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.SourceGenerators.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for collections.
/// </summary>
public static class CollectionExtensions
{
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

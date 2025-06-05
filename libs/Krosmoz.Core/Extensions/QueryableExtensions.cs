// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Collections.Frozen;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for converting IQueryable collections to thread-safe collections
/// such as ConcurrentBag and ConcurrentDictionary.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Converts an IQueryable source to a ConcurrentBag asynchronously.
    /// </summary>
    /// <typeparam name="TValue">The type of elements in the source.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ConcurrentBag of the source elements.</returns>
    public static async Task<ConcurrentBag<TValue>> ToConcurrentBagAsync<TValue>(this IQueryable<TValue> source)
        where TValue : class
    {
        return new ConcurrentBag<TValue>(await source.ToListAsync());
    }

    /// <summary>
    /// Converts an IQueryable source to a ConcurrentBag asynchronously with a cancellation token.
    /// </summary>
    /// <typeparam name="TValue">The type of elements in the source.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ConcurrentBag of the source elements.</returns>
    public static async Task<ConcurrentBag<TValue>> ToConcurrentBagAsync<TValue>(this IQueryable<TValue> source, CancellationToken cancellationToken)
        where TValue : class
    {
        return new ConcurrentBag<TValue>(await source.ToListAsync(cancellationToken));
    }

    /// <summary>
    /// Converts an IQueryable source to a ConcurrentDictionary asynchronously.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ConcurrentDictionary of the source elements.</returns>
    public static async Task<ConcurrentDictionary<TKey, TValue>> ToConcurrentDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector)
        where TKey : notnull
        where TValue : class
    {
        return new ConcurrentDictionary<TKey, TValue>(await source.ToDictionaryAsync(keySelector));
    }

    /// <summary>
    /// Converts an IQueryable source to a ConcurrentDictionary asynchronously with a cancellation token.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ConcurrentDictionary of the source elements.</returns>
    public static async Task<ConcurrentDictionary<TKey, TValue>> ToConcurrentDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, CancellationToken cancellationToken)
        where TKey : notnull
        where TValue : class
    {
        return new ConcurrentDictionary<TKey, TValue>(await source.ToDictionaryAsync(keySelector, cancellationToken));
    }

    /// <summary>
    /// Converts an IQueryable source to a ConcurrentDictionary asynchronously with a custom comparer.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="comparer">An IEqualityComparer to compare keys.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ConcurrentDictionary of the source elements.</returns>
    public static async Task<ConcurrentDictionary<TKey, TValue>> ToConcurrentDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
        where TKey : notnull
        where TValue : class
    {
        return new ConcurrentDictionary<TKey, TValue>(await source.ToDictionaryAsync(keySelector, comparer));
    }

    /// <summary>
    /// Converts an IQueryable source to a ConcurrentDictionary asynchronously with a custom comparer and a cancellation token.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="comparer">An IEqualityComparer to compare keys.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ConcurrentDictionary of the source elements.</returns>
    public static async Task<ConcurrentDictionary<TKey, TValue>> ToConcurrentDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
        where TKey : notnull
        where TValue : class
    {
        return new ConcurrentDictionary<TKey, TValue>(await source.ToDictionaryAsync(keySelector, comparer, cancellationToken));
    }

    /// <summary>
    /// Converts an IQueryable source to a ConcurrentDictionary asynchronously with a custom value selector.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="valueSelector">A function to extract a value from each element.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ConcurrentDictionary of the source elements.</returns>
    public static async Task<ConcurrentDictionary<TKey, TValue>> ToConcurrentDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector)
        where TKey : notnull
        where TValue : class
    {
        return new ConcurrentDictionary<TKey, TValue>(await source.ToDictionaryAsync(keySelector, valueSelector));
    }

    /// <summary>
    /// Converts an IQueryable source to a ConcurrentDictionary asynchronously with a custom value selector and a cancellation token.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="valueSelector">A function to extract a value from each element.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ConcurrentDictionary of the source elements.</returns>
    public static async Task<ConcurrentDictionary<TKey, TValue>> ToConcurrentDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector, CancellationToken cancellationToken)
        where TKey : notnull
        where TValue : class
    {
        return new ConcurrentDictionary<TKey, TValue>(await source.ToDictionaryAsync(keySelector, valueSelector, cancellationToken));
    }

    /// <summary>
    /// Converts an IQueryable source to a ConcurrentDictionary asynchronously with a custom value selector and comparer.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="valueSelector">A function to extract a value from each element.</param>
    /// <param name="comparer">An IEqualityComparer to compare keys.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ConcurrentDictionary of the source elements.</returns>
    public static async Task<ConcurrentDictionary<TKey, TValue>> ToConcurrentDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector, IEqualityComparer<TKey> comparer)
        where TKey : notnull
        where TValue : class
    {
        return new ConcurrentDictionary<TKey, TValue>(await source.ToDictionaryAsync(keySelector, valueSelector, comparer));
    }

    /// <summary>
    /// Converts an IQueryable source to a ConcurrentDictionary asynchronously with a custom value selector, comparer, and a cancellation token.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="valueSelector">A function to extract a value from each element.</param>
    /// <param name="comparer">An IEqualityComparer to compare keys.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ConcurrentDictionary of the source elements.</returns>
    public static async Task<ConcurrentDictionary<TKey, TValue>> ToConcurrentDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
        where TKey : notnull
        where TValue : class
    {
        return new ConcurrentDictionary<TKey, TValue>(await source.ToDictionaryAsync(keySelector, valueSelector, comparer, cancellationToken));
    }

    /// <summary>
    /// Converts an IQueryable source to a FrozenDictionary asynchronously.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a FrozenDictionary of the source elements.</returns>
    public static async Task<FrozenDictionary<TKey, TValue>> ToFrozenDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector)
        where TKey : notnull
        where TValue : class
    {
        return (await source.ToDictionaryAsync(keySelector)).ToFrozenDictionary();
    }

    /// <summary>
    /// Converts an IQueryable source to a FrozenDictionary asynchronously with a cancellation token.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a FrozenDictionary of the source elements.</returns>
    public static async Task<FrozenDictionary<TKey, TValue>> ToFrozenDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, CancellationToken cancellationToken)
        where TKey : notnull
        where TValue : class
    {
        return (await source.ToDictionaryAsync(keySelector, cancellationToken)).ToFrozenDictionary();
    }

    /// <summary>
    /// Converts an IQueryable source to a FrozenDictionary asynchronously with a custom comparer.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="comparer">An IEqualityComparer to compare keys.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a FrozenDictionary of the source elements.</returns>
    public static async Task<FrozenDictionary<TKey, TValue>> ToFrozenDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
        where TKey : notnull
        where TValue : class
    {
        return (await source.ToDictionaryAsync(keySelector, comparer)).ToFrozenDictionary();
    }

    /// <summary>
    /// Converts an IQueryable source to a FrozenDictionary asynchronously with a custom comparer and a cancellation token.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="comparer">An IEqualityComparer to compare keys.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a FrozenDictionary of the source elements.</returns>
    public static async Task<FrozenDictionary<TKey, TValue>> ToFrozenDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
        where TKey : notnull
        where TValue : class
    {
        return (await source.ToDictionaryAsync(keySelector, comparer, cancellationToken)).ToFrozenDictionary();
    }

    /// <summary>
    /// Converts an IQueryable source to a FrozenDictionary asynchronously with a custom value selector.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="valueSelector">A function to extract a value from each element.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a FrozenDictionary of the source elements.</returns>
    public static async Task<FrozenDictionary<TKey, TValue>> ToFrozenDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector)
        where TKey : notnull
        where TValue : class
    {
        return (await source.ToDictionaryAsync(keySelector, valueSelector)).ToFrozenDictionary();
    }

    /// <summary>
    /// Converts an IQueryable source to a FrozenDictionary asynchronously with a custom value selector and a cancellation token.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="valueSelector">A function to extract a value from each element.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a FrozenDictionary of the source elements.</returns>
    public static async Task<FrozenDictionary<TKey, TValue>> ToFrozenDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector, CancellationToken cancellationToken)
        where TKey : notnull
        where TValue : class
    {
        return (await source.ToDictionaryAsync(keySelector, valueSelector, cancellationToken)).ToFrozenDictionary();
    }

    /// <summary>
    /// Converts an IQueryable source to a FrozenDictionary asynchronously with a custom value selector and comparer.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="valueSelector">A function to extract a value from each element.</param>
    /// <param name="comparer">An IEqualityComparer to compare keys.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a FrozenDictionary of the source elements.</returns>
    public static async Task<FrozenDictionary<TKey, TValue>> ToFrozenDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector, IEqualityComparer<TKey> comparer)
        where TKey : notnull
        where TValue : class
    {
        return (await source.ToDictionaryAsync(keySelector, valueSelector, comparer)).ToFrozenDictionary();
    }

    /// <summary>
    /// Converts an IQueryable source to a FrozenDictionary asynchronously with a custom value selector, comparer, and a cancellation token.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="source">The IQueryable source to convert.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="valueSelector">A function to extract a value from each element.</param>
    /// <param name="comparer">An IEqualityComparer to compare keys.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a FrozenDictionary of the source elements.</returns>
    public static async Task<FrozenDictionary<TKey, TValue>> ToFrozenDictionaryAsync<TKey, TValue>(this IQueryable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TValue> valueSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
        where TKey : notnull
        where TValue : class
    {
        return (await source.ToDictionaryAsync(keySelector, valueSelector, comparer, cancellationToken)).ToFrozenDictionary();
    }
}

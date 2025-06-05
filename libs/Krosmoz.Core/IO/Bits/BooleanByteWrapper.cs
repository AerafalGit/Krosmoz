// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Contracts;

namespace Krosmoz.Core.IO.Bits;

/// <summary>
/// Provides utility methods for manipulating individual bits within a byte.
/// </summary>
public static class BooleanByteWrapper
{
    private const int UInt8Bits = 8;

    /// <summary>
    /// Sets or clears a specific bit (flag) in a byte at the given offset.
    /// </summary>
    /// <param name="flag">The byte to modify.</param>
    /// <param name="offset">The bit position (0-7) to set or clear.</param>
    /// <param name="value">The value to set the bit to (true to set, false to clear).</param>
    /// <returns>The modified byte with the specified bit set or cleared.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the offset is greater than or equal to 8.</exception>
    [Pure]
    public static byte SetFlag(byte flag, byte offset, bool value)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, UInt8Bits);

        return value ? (byte)(flag | (1 << offset)) : (byte)(flag & (byte.MaxValue - (1 << offset)));
    }

    /// <summary>
    /// Retrieves the value of a specific bit (flag) in a byte at the given offset.
    /// </summary>
    /// <param name="flag">The byte to read from.</param>
    /// <param name="offset">The bit position (0-7) to retrieve.</param>
    /// <returns>True if the specified bit is set, otherwise false.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the offset is greater than or equal to 8.</exception>
    [Pure]
    public static bool GetFlag(byte flag, byte offset)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, UInt8Bits);

        return (flag & (byte)(1 << offset)) is not 0;
    }
}

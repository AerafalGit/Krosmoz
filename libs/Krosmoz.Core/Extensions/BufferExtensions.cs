// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Runtime.InteropServices;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for working with memory buffers.
/// </summary>
internal static class BufferExtensions
{
    /// <summary>
    /// Converts a <see cref="ReadOnlyMemory{T}"/> of bytes to an <see cref="ArraySegment{T}"/>.
    /// </summary>
    /// <param name="memory">The read-only memory buffer to convert.</param>
    /// <returns>An <see cref="ArraySegment{T}"/> representing the read-only memory buffer.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the memory buffer is not backed by an array.</exception>
    public static ArraySegment<byte> GetArray(this ReadOnlyMemory<byte> memory)
    {
        if (!MemoryMarshal.TryGetArray(memory, out var result))
            throw new InvalidOperationException("Buffer backed by array was expected.");

        return result;
    }
}

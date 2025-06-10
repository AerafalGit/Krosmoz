// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Metadata;

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Represents the result of reading a frame, including the decoded message and status flags.
/// </summary>
/// <param name="MessageLength">The length of the decoded message.</param>
/// <param name="MessageName">The name of the decoded message, if available.</param>
/// <param name="Message">The decoded message contained in the frame.</param>
/// <param name="IsCanceled">Indicates whether the read operation was canceled.</param>
/// <param name="IsCompleted">Indicates whether the read operation was completed.</param>
public record struct FrameReadResult(int MessageLength, string? MessageName, DofusMessage? Message, bool IsCanceled, bool IsCompleted)
{
    /// <summary>
    /// Gets an empty instance of the <see cref="FrameReadResult"/> struct.
    /// </summary>
    public static FrameReadResult Empty =>
        default;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameReadResult"/> struct
    /// with the specified cancellation and completion status, but no message.
    /// </summary>
    /// <param name="isCanceled">Indicates whether the read operation was canceled.</param>
    /// <param name="isCompleted">Indicates whether the read operation was completed.</param>
    public FrameReadResult(bool isCanceled, bool isCompleted) : this(0, null, null, isCanceled, isCompleted)
    {
    }
}

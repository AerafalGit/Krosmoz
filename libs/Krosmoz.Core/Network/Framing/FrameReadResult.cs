// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Represents the result of reading a frame, including the decoded message and status flags.
/// </summary>
/// <param name="Message">The decoded message contained in the frame.</param>
/// <param name="IsCanceled">Indicates whether the read operation was canceled.</param>
/// <param name="IsCompleted">Indicates whether the read operation was completed.</param>
/// <typeparam name="TMessage">The type of the message contained in the frame.</typeparam>
public record struct FrameReadResult<TMessage>(TMessage? Message, bool IsCanceled, bool IsCompleted)
    where TMessage : class
{
    /// <summary>
    /// Gets an empty instance of the <see cref="FrameReadResult{TMessage}"/> struct.
    /// </summary>
    public static FrameReadResult<TMessage> Empty =>
        default;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameReadResult{TMessage}"/> struct
    /// with the specified cancellation and completion status, but no message.
    /// </summary>
    /// <param name="isCanceled">Indicates whether the read operation was canceled.</param>
    /// <param name="isCompleted">Indicates whether the read operation was completed.</param>
    public FrameReadResult(bool isCanceled, bool isCompleted) : this(null, isCanceled, isCompleted)
    {
    }
}

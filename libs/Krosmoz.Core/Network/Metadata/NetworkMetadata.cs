// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Network.Metadata;

/// <summary>
/// Represents metadata for a network operation involving a specific message type.
/// </summary>
/// <typeparam name="TMessage">The type of the network message.</typeparam>
public readonly struct NetworkMetadata<TMessage>
    where TMessage : NetworkMessage
{
    /// <summary>
    /// Gets the network message associated with the metadata, if any.
    /// </summary>
    public TMessage? Message { get; }

    /// <summary>
    /// Gets a value indicating whether the network operation was canceled.
    /// </summary>
    public bool IsCanceled { get; }

    /// <summary>
    /// Gets a value indicating whether the network operation was completed.
    /// </summary>
    public bool IsCompleted { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkMetadata{TMessage}"/> struct
    /// with the specified cancellation and completion states.
    /// </summary>
    /// <param name="isCanceled">Indicates whether the network operation was canceled.</param>
    /// <param name="isCompleted">Indicates whether the network operation was completed.</param>
    public NetworkMetadata(bool isCanceled, bool isCompleted)
    {
        IsCanceled = isCanceled;
        IsCompleted = isCompleted;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkMetadata{TMessage}"/> struct
    /// with the specified message, cancellation state, and completion state.
    /// </summary>
    /// <param name="message">The network message associated with the metadata.</param>
    /// <param name="isCanceled">Indicates whether the network operation was canceled.</param>
    /// <param name="isCompleted">Indicates whether the network operation was completed.</param>
    public NetworkMetadata(TMessage message, bool isCanceled, bool isCompleted)
    {
        IsCanceled = isCanceled;
        IsCompleted = isCompleted;
        Message = message;
    }
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Framing.Serialization;
using Krosmoz.Core.Network.Metadata;
using Krosmoz.Core.Network.Transport;
using Microsoft.AspNetCore.Connections;

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Provides extension methods for creating frame readers and writers
/// from a <see cref="ConnectionContext"/>.
/// </summary>
public static class FrameExtensions
{
    /// <summary>
    /// Creates a <see cref="FrameReader"/> for the specified connection context.
    /// </summary>
    /// <param name="context">The connection context.</param>
    /// <returns>A new instance of <see cref="FrameReader"/>.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if required features are not available in the connection context.
    /// </exception>
    public static FrameReader CreateReader(this ConnectionContext context)
    {
        var decoder = context.Features.Get<MessageDecoder>()!;
        var factory = context.Features.Get<IMessageFactory>()!;
        var metrics = context.Features.Get<SocketConnectionMetrics>()!;

        return new FrameReader(context.Transport.Input, decoder, factory, metrics);
    }

    /// <summary>
    /// Creates a <see cref="FrameWriter"/> for the specified connection context.
    /// </summary>
    /// <param name="context">The connection context.</param>
    /// <returns>A new instance of <see cref="FrameWriter"/>.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if required features are not available in the connection context.
    /// </exception>
    public static FrameWriter CreateWriter(this ConnectionContext context)
    {
        var factory = context.Features.Get<IMessageFactory>()!;
        var metrics = context.Features.Get<SocketConnectionMetrics>()!;

        return new FrameWriter(context.Transport.Output, factory, metrics);
    }
}

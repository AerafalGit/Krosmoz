// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Framing.Serialization;
using Microsoft.AspNetCore.Connections;

namespace Krosmoz.Core.Network.Framing;

/// <summary>
/// Provides static methods for creating frame readers and writers for a connection context.
/// </summary>
public static class FrameExtensions
{
    /// <summary>
    /// Creates a <see cref="FrameReader{TMessage}"/> for the specified connection context and decoder.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message to decode.</typeparam>
    /// <param name="context">The connection context to read frames from.</param>
    /// <param name="decoder">The decoder to use for decoding messages.</param>
    /// <returns>A <see cref="FrameReader{TMessage}"/> instance.</returns>
    public static FrameReader<TMessage> CreateReader<TMessage>(this ConnectionContext context, IMessageDecoder<TMessage> decoder)
        where TMessage : class
    {
        return new FrameReader<TMessage>(context.Transport.Input, decoder);
    }

    /// <summary>
    /// Creates a <see cref="FrameWriter{TMessage}"/> for the specified connection context and encoder.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message to encode.</typeparam>
    /// <param name="context">The connection context to write frames to.</param>
    /// <param name="encoder">The encoder to use for encoding messages.</param>
    /// <returns>A <see cref="FrameWriter{TMessage}"/> instance.</returns>
    public static FrameWriter<TMessage> CreateWriter<TMessage>(this ConnectionContext context, IMessageEncoder<TMessage> encoder)
        where TMessage : class
    {
        return new FrameWriter<TMessage>(context.Transport.Output, encoder);
    }

    /// <summary>
    /// Creates a pair of <see cref="FrameReader{TMessage}"/> and <see cref="FrameWriter{TMessage}"/> for the specified connection context.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message to encode and decode.</typeparam>
    /// <param name="context">The connection context to read and write frames.</param>
    /// <param name="decoder">The decoder to use for decoding messages.</param>
    /// <param name="encoder">The encoder to use for encoding messages.</param>
    /// <returns>A tuple containing the <see cref="FrameReader{TMessage}"/> and <see cref="FrameWriter{TMessage}"/>.</returns>
    public static (FrameReader<TMessage> Reader, FrameWriter<TMessage> Writer) CreateReaderWriterPair<TMessage>(this ConnectionContext context, IMessageDecoder<TMessage> decoder, IMessageEncoder<TMessage> encoder)
        where TMessage : class
    {
        return (context.CreateReader(decoder), context.CreateWriter(encoder));
    }
}

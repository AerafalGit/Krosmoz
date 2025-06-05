// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Compression;

namespace Krosmoz.Protocol.Messages.Common;

public sealed class NetworkDataContainerMessage : DofusMessage
{
	public new const uint StaticProtocolId = 2;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NetworkDataContainerMessage Empty =>
		new() { Content = [] };

	public required byte[] Content { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarInt32(Content.Length);
		writer.WriteSpan(Content);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Content = ZipCompressor.UncompressV2(reader.ReadBytes(reader.ReadVarInt32()));
	}
}

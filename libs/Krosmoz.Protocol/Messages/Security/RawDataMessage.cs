// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Security;

public sealed class RawDataMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6253;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static RawDataMessage Empty =>
		new() { Content = [] };

	public required byte[] Content { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarInt32(Content.Length);
		writer.WriteSpan(Content);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Content = reader.ReadBytes(reader.ReadVarInt32());
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Security;

public sealed class CheckFileRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6154;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CheckFileRequestMessage Empty =>
		new() { Filename = string.Empty, Type = 0 };

	public required string Filename { get; set; }

	public required sbyte Type { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Filename);
		writer.WriteInt8(Type);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Filename = reader.ReadUtfPrefixedLength16();
		Type = reader.ReadInt8();
	}
}

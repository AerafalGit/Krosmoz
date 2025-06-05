// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Security;

public sealed class CheckFileMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6156;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CheckFileMessage Empty =>
		new() { FilenameHash = string.Empty, Type = 0, Value = string.Empty };

	public required string FilenameHash { get; set; }

	public required sbyte Type { get; set; }

	public required string Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(FilenameHash);
		writer.WriteInt8(Type);
		writer.WriteUtfPrefixedLength16(Value);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FilenameHash = reader.ReadUtfPrefixedLength16();
		Type = reader.ReadInt8();
		Value = reader.ReadUtfPrefixedLength16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection;

public sealed class HelloConnectMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HelloConnectMessage Empty =>
		new() { Salt = string.Empty, Key = [] };

	public required string Salt { get; set; }

	public required IEnumerable<sbyte> Key { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Salt);
		var keyBefore = writer.Position;
		var keyCount = 0;
		writer.WriteVarInt32(0);
		foreach (var item in Key)
		{
			writer.WriteInt8(item);
			keyCount++;
		}
		var keyAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, keyBefore);
		writer.WriteVarInt32(keyCount);
		writer.Seek(SeekOrigin.Begin, keyAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Salt = reader.ReadUtfPrefixedLength16();
		var keyCount = reader.ReadVarInt32();
		var key = new sbyte[keyCount];
		for (var i = 0; i < keyCount; i++)
		{
			key[i] = reader.ReadInt8();
		}
		Key = key;
	}
}

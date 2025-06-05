// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Security;

public sealed class ClientKeyMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5607;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ClientKeyMessage Empty =>
		new() { Key = string.Empty };

	public required string Key { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Key);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Key = reader.ReadUtfPrefixedLength16();
	}
}

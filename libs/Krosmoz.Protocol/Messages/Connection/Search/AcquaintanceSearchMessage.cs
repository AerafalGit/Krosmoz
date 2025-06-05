// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection.Search;

public sealed class AcquaintanceSearchMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6144;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AcquaintanceSearchMessage Empty =>
		new() { Nickname = string.Empty };

	public required string Nickname { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Nickname);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Nickname = reader.ReadUtfPrefixedLength16();
	}
}

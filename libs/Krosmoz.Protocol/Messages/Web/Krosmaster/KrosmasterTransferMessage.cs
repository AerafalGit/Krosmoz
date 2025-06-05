// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Web.Krosmaster;

public sealed class KrosmasterTransferMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6348;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static KrosmasterTransferMessage Empty =>
		new() { Uid = string.Empty, Failure = 0 };

	public required string Uid { get; set; }

	public required sbyte Failure { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Uid);
		writer.WriteInt8(Failure);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Uid = reader.ReadUtfPrefixedLength16();
		Failure = reader.ReadInt8();
	}
}

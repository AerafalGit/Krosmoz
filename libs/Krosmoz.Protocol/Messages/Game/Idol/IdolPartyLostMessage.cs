// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Idol;

public sealed class IdolPartyLostMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6580;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IdolPartyLostMessage Empty =>
		new() { IdolId = 0 };

	public required ushort IdolId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(IdolId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		IdolId = reader.ReadVarUInt16();
	}
}

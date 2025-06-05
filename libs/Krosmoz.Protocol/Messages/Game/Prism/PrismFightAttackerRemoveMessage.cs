// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismFightAttackerRemoveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5897;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismFightAttackerRemoveMessage Empty =>
		new() { SubAreaId = 0, FightId = 0, FighterToRemoveId = 0 };

	public required ushort SubAreaId { get; set; }

	public required ushort FightId { get; set; }

	public required uint FighterToRemoveId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteVarUInt16(FightId);
		writer.WriteVarUInt32(FighterToRemoveId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadVarUInt16();
		FightId = reader.ReadVarUInt16();
		FighterToRemoveId = reader.ReadVarUInt32();
	}
}

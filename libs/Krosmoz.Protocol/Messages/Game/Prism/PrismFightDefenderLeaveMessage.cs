// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismFightDefenderLeaveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5892;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismFightDefenderLeaveMessage Empty =>
		new() { SubAreaId = 0, FightId = 0, FighterToRemoveId = 0 };

	public required short SubAreaId { get; set; }

	public required double FightId { get; set; }

	public required int FighterToRemoveId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(SubAreaId);
		writer.WriteDouble(FightId);
		writer.WriteInt(FighterToRemoveId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadShort();
		FightId = reader.ReadDouble();
		FighterToRemoveId = reader.ReadInt();
	}
}

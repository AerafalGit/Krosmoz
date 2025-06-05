// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight.Arena;

public sealed class GameRolePlayArenaUpdatePlayerInfosMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6301;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayArenaUpdatePlayerInfosMessage Empty =>
		new() { Rank = 0, BestDailyRank = 0, BestRank = 0, VictoryCount = 0, ArenaFightcount = 0 };

	public required ushort Rank { get; set; }

	public required ushort BestDailyRank { get; set; }

	public required ushort BestRank { get; set; }

	public required ushort VictoryCount { get; set; }

	public required ushort ArenaFightcount { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Rank);
		writer.WriteVarUInt16(BestDailyRank);
		writer.WriteVarUInt16(BestRank);
		writer.WriteVarUInt16(VictoryCount);
		writer.WriteVarUInt16(ArenaFightcount);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Rank = reader.ReadVarUInt16();
		BestDailyRank = reader.ReadVarUInt16();
		BestRank = reader.ReadVarUInt16();
		VictoryCount = reader.ReadVarUInt16();
		ArenaFightcount = reader.ReadVarUInt16();
	}
}

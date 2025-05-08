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

	public required short Rank { get; set; }

	public required short BestDailyRank { get; set; }

	public required short BestRank { get; set; }

	public required short VictoryCount { get; set; }

	public required short ArenaFightcount { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(Rank);
		writer.WriteShort(BestDailyRank);
		writer.WriteShort(BestRank);
		writer.WriteShort(VictoryCount);
		writer.WriteShort(ArenaFightcount);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Rank = reader.ReadShort();
		BestDailyRank = reader.ReadShort();
		BestRank = reader.ReadShort();
		VictoryCount = reader.ReadShort();
		ArenaFightcount = reader.ReadShort();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character;

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class GuildFightPlayersEnemiesListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5928;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildFightPlayersEnemiesListMessage Empty =>
		new() { FightId = 0, PlayerInfo = [] };

	public required int FightId { get; set; }

	public required IEnumerable<CharacterMinimalPlusLookInformations> PlayerInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(FightId);
		var playerInfoBefore = writer.Position;
		var playerInfoCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PlayerInfo)
		{
			item.Serialize(writer);
			playerInfoCount++;
		}
		var playerInfoAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, playerInfoBefore);
		writer.WriteInt16((short)playerInfoCount);
		writer.Seek(SeekOrigin.Begin, playerInfoAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt32();
		var playerInfoCount = reader.ReadInt16();
		var playerInfo = new CharacterMinimalPlusLookInformations[playerInfoCount];
		for (var i = 0; i < playerInfoCount; i++)
		{
			var entry = CharacterMinimalPlusLookInformations.Empty;
			entry.Deserialize(reader);
			playerInfo[i] = entry;
		}
		PlayerInfo = playerInfo;
	}
}

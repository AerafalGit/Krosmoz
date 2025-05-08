// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class DungeonPartyFinderRoomContentUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6250;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DungeonPartyFinderRoomContentUpdateMessage Empty =>
		new() { DungeonId = 0, AddedPlayers = [], RemovedPlayersIds = [] };

	public required short DungeonId { get; set; }

	public required IEnumerable<DungeonPartyFinderPlayer> AddedPlayers { get; set; }

	public required IEnumerable<int> RemovedPlayersIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(DungeonId);
		var addedPlayersBefore = writer.Position;
		var addedPlayersCount = 0;
		writer.WriteShort(0);
		foreach (var item in AddedPlayers)
		{
			item.Serialize(writer);
			addedPlayersCount++;
		}
		var addedPlayersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, addedPlayersBefore);
		writer.WriteShort((short)addedPlayersCount);
		writer.Seek(SeekOrigin.Begin, addedPlayersAfter);
		var removedPlayersIdsBefore = writer.Position;
		var removedPlayersIdsCount = 0;
		writer.WriteShort(0);
		foreach (var item in RemovedPlayersIds)
		{
			writer.WriteInt(item);
			removedPlayersIdsCount++;
		}
		var removedPlayersIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, removedPlayersIdsBefore);
		writer.WriteShort((short)removedPlayersIdsCount);
		writer.Seek(SeekOrigin.Begin, removedPlayersIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DungeonId = reader.ReadShort();
		var addedPlayersCount = reader.ReadShort();
		var addedPlayers = new DungeonPartyFinderPlayer[addedPlayersCount];
		for (var i = 0; i < addedPlayersCount; i++)
		{
			var entry = DungeonPartyFinderPlayer.Empty;
			entry.Deserialize(reader);
			addedPlayers[i] = entry;
		}
		AddedPlayers = addedPlayers;
		var removedPlayersIdsCount = reader.ReadShort();
		var removedPlayersIds = new int[removedPlayersIdsCount];
		for (var i = 0; i < removedPlayersIdsCount; i++)
		{
			removedPlayersIds[i] = reader.ReadInt();
		}
		RemovedPlayersIds = removedPlayersIds;
	}
}

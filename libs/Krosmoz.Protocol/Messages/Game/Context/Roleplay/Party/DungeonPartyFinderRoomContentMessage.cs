// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class DungeonPartyFinderRoomContentMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6247;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DungeonPartyFinderRoomContentMessage Empty =>
		new() { DungeonId = 0, Players = [] };

	public required ushort DungeonId { get; set; }

	public required IEnumerable<DungeonPartyFinderPlayer> Players { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(DungeonId);
		var playersBefore = writer.Position;
		var playersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Players)
		{
			item.Serialize(writer);
			playersCount++;
		}
		var playersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, playersBefore);
		writer.WriteInt16((short)playersCount);
		writer.Seek(SeekOrigin.Begin, playersAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DungeonId = reader.ReadVarUInt16();
		var playersCount = reader.ReadInt16();
		var players = new DungeonPartyFinderPlayer[playersCount];
		for (var i = 0; i < playersCount; i++)
		{
			var entry = DungeonPartyFinderPlayer.Empty;
			entry.Deserialize(reader);
			players[i] = entry;
		}
		Players = players;
	}
}

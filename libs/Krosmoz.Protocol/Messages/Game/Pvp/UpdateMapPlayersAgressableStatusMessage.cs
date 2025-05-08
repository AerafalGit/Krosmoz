// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Pvp;

public sealed class UpdateMapPlayersAgressableStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6454;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static UpdateMapPlayersAgressableStatusMessage Empty =>
		new() { PlayerIds = [], Enable = [] };

	public required IEnumerable<int> PlayerIds { get; set; }

	public required IEnumerable<sbyte> Enable { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var playerIdsBefore = writer.Position;
		var playerIdsCount = 0;
		writer.WriteShort(0);
		foreach (var item in PlayerIds)
		{
			writer.WriteInt(item);
			playerIdsCount++;
		}
		var playerIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, playerIdsBefore);
		writer.WriteShort((short)playerIdsCount);
		writer.Seek(SeekOrigin.Begin, playerIdsAfter);
		var enableBefore = writer.Position;
		var enableCount = 0;
		writer.WriteShort(0);
		foreach (var item in Enable)
		{
			writer.WriteSByte(item);
			enableCount++;
		}
		var enableAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, enableBefore);
		writer.WriteShort((short)enableCount);
		writer.Seek(SeekOrigin.Begin, enableAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var playerIdsCount = reader.ReadShort();
		var playerIds = new int[playerIdsCount];
		for (var i = 0; i < playerIdsCount; i++)
		{
			playerIds[i] = reader.ReadInt();
		}
		PlayerIds = playerIds;
		var enableCount = reader.ReadShort();
		var enable = new sbyte[enableCount];
		for (var i = 0; i < enableCount; i++)
		{
			enable[i] = reader.ReadSByte();
		}
		Enable = enable;
	}
}

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

	public required IEnumerable<uint> PlayerIds { get; set; }

	public required IEnumerable<sbyte> Enable { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var playerIdsBefore = writer.Position;
		var playerIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PlayerIds)
		{
			writer.WriteVarUInt32(item);
			playerIdsCount++;
		}
		var playerIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, playerIdsBefore);
		writer.WriteInt16((short)playerIdsCount);
		writer.Seek(SeekOrigin.Begin, playerIdsAfter);
		var enableBefore = writer.Position;
		var enableCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Enable)
		{
			writer.WriteInt8(item);
			enableCount++;
		}
		var enableAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, enableBefore);
		writer.WriteInt16((short)enableCount);
		writer.Seek(SeekOrigin.Begin, enableAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var playerIdsCount = reader.ReadInt16();
		var playerIds = new uint[playerIdsCount];
		for (var i = 0; i < playerIdsCount; i++)
		{
			playerIds[i] = reader.ReadVarUInt32();
		}
		PlayerIds = playerIds;
		var enableCount = reader.ReadInt16();
		var enable = new sbyte[enableCount];
		for (var i = 0; i < enableCount; i++)
		{
			enable[i] = reader.ReadInt8();
		}
		Enable = enable;
	}
}

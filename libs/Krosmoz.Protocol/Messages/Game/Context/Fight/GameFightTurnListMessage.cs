// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightTurnListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 713;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightTurnListMessage Empty =>
		new() { Ids = [], DeadsIds = [] };

	public required IEnumerable<int> Ids { get; set; }

	public required IEnumerable<int> DeadsIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var idsBefore = writer.Position;
		var idsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Ids)
		{
			writer.WriteInt32(item);
			idsCount++;
		}
		var idsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, idsBefore);
		writer.WriteInt16((short)idsCount);
		writer.Seek(SeekOrigin.Begin, idsAfter);
		var deadsIdsBefore = writer.Position;
		var deadsIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in DeadsIds)
		{
			writer.WriteInt32(item);
			deadsIdsCount++;
		}
		var deadsIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, deadsIdsBefore);
		writer.WriteInt16((short)deadsIdsCount);
		writer.Seek(SeekOrigin.Begin, deadsIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var idsCount = reader.ReadInt16();
		var ids = new int[idsCount];
		for (var i = 0; i < idsCount; i++)
		{
			ids[i] = reader.ReadInt32();
		}
		Ids = ids;
		var deadsIdsCount = reader.ReadInt16();
		var deadsIds = new int[deadsIdsCount];
		for (var i = 0; i < deadsIdsCount; i++)
		{
			deadsIds[i] = reader.ReadInt32();
		}
		DeadsIds = deadsIds;
	}
}

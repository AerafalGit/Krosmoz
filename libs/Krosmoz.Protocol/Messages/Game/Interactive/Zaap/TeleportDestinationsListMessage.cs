// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive.Zaap;

public class TeleportDestinationsListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5960;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TeleportDestinationsListMessage Empty =>
		new() { TeleporterType = 0, MapIds = [], SubAreaIds = [], Costs = [], DestTeleporterType = [] };

	public required sbyte TeleporterType { get; set; }

	public required IEnumerable<int> MapIds { get; set; }

	public required IEnumerable<short> SubAreaIds { get; set; }

	public required IEnumerable<short> Costs { get; set; }

	public required IEnumerable<sbyte> DestTeleporterType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(TeleporterType);
		var mapIdsBefore = writer.Position;
		var mapIdsCount = 0;
		writer.WriteShort(0);
		foreach (var item in MapIds)
		{
			writer.WriteInt(item);
			mapIdsCount++;
		}
		var mapIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, mapIdsBefore);
		writer.WriteShort((short)mapIdsCount);
		writer.Seek(SeekOrigin.Begin, mapIdsAfter);
		var subAreaIdsBefore = writer.Position;
		var subAreaIdsCount = 0;
		writer.WriteShort(0);
		foreach (var item in SubAreaIds)
		{
			writer.WriteShort(item);
			subAreaIdsCount++;
		}
		var subAreaIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, subAreaIdsBefore);
		writer.WriteShort((short)subAreaIdsCount);
		writer.Seek(SeekOrigin.Begin, subAreaIdsAfter);
		var costsBefore = writer.Position;
		var costsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Costs)
		{
			writer.WriteShort(item);
			costsCount++;
		}
		var costsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, costsBefore);
		writer.WriteShort((short)costsCount);
		writer.Seek(SeekOrigin.Begin, costsAfter);
		var destTeleporterTypeBefore = writer.Position;
		var destTeleporterTypeCount = 0;
		writer.WriteShort(0);
		foreach (var item in DestTeleporterType)
		{
			writer.WriteSByte(item);
			destTeleporterTypeCount++;
		}
		var destTeleporterTypeAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, destTeleporterTypeBefore);
		writer.WriteShort((short)destTeleporterTypeCount);
		writer.Seek(SeekOrigin.Begin, destTeleporterTypeAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TeleporterType = reader.ReadSByte();
		var mapIdsCount = reader.ReadShort();
		var mapIds = new int[mapIdsCount];
		for (var i = 0; i < mapIdsCount; i++)
		{
			mapIds[i] = reader.ReadInt();
		}
		MapIds = mapIds;
		var subAreaIdsCount = reader.ReadShort();
		var subAreaIds = new short[subAreaIdsCount];
		for (var i = 0; i < subAreaIdsCount; i++)
		{
			subAreaIds[i] = reader.ReadShort();
		}
		SubAreaIds = subAreaIds;
		var costsCount = reader.ReadShort();
		var costs = new short[costsCount];
		for (var i = 0; i < costsCount; i++)
		{
			costs[i] = reader.ReadShort();
		}
		Costs = costs;
		var destTeleporterTypeCount = reader.ReadShort();
		var destTeleporterType = new sbyte[destTeleporterTypeCount];
		for (var i = 0; i < destTeleporterTypeCount; i++)
		{
			destTeleporterType[i] = reader.ReadSByte();
		}
		DestTeleporterType = destTeleporterType;
	}
}

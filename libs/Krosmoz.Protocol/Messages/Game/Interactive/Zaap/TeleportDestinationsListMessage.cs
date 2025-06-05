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

	public required IEnumerable<ushort> SubAreaIds { get; set; }

	public required IEnumerable<ushort> Costs { get; set; }

	public required IEnumerable<sbyte> DestTeleporterType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(TeleporterType);
		var mapIdsBefore = writer.Position;
		var mapIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in MapIds)
		{
			writer.WriteInt32(item);
			mapIdsCount++;
		}
		var mapIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, mapIdsBefore);
		writer.WriteInt16((short)mapIdsCount);
		writer.Seek(SeekOrigin.Begin, mapIdsAfter);
		var subAreaIdsBefore = writer.Position;
		var subAreaIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SubAreaIds)
		{
			writer.WriteVarUInt16(item);
			subAreaIdsCount++;
		}
		var subAreaIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, subAreaIdsBefore);
		writer.WriteInt16((short)subAreaIdsCount);
		writer.Seek(SeekOrigin.Begin, subAreaIdsAfter);
		var costsBefore = writer.Position;
		var costsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Costs)
		{
			writer.WriteVarUInt16(item);
			costsCount++;
		}
		var costsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, costsBefore);
		writer.WriteInt16((short)costsCount);
		writer.Seek(SeekOrigin.Begin, costsAfter);
		var destTeleporterTypeBefore = writer.Position;
		var destTeleporterTypeCount = 0;
		writer.WriteInt16(0);
		foreach (var item in DestTeleporterType)
		{
			writer.WriteInt8(item);
			destTeleporterTypeCount++;
		}
		var destTeleporterTypeAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, destTeleporterTypeBefore);
		writer.WriteInt16((short)destTeleporterTypeCount);
		writer.Seek(SeekOrigin.Begin, destTeleporterTypeAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TeleporterType = reader.ReadInt8();
		var mapIdsCount = reader.ReadInt16();
		var mapIds = new int[mapIdsCount];
		for (var i = 0; i < mapIdsCount; i++)
		{
			mapIds[i] = reader.ReadInt32();
		}
		MapIds = mapIds;
		var subAreaIdsCount = reader.ReadInt16();
		var subAreaIds = new ushort[subAreaIdsCount];
		for (var i = 0; i < subAreaIdsCount; i++)
		{
			subAreaIds[i] = reader.ReadVarUInt16();
		}
		SubAreaIds = subAreaIds;
		var costsCount = reader.ReadInt16();
		var costs = new ushort[costsCount];
		for (var i = 0; i < costsCount; i++)
		{
			costs[i] = reader.ReadVarUInt16();
		}
		Costs = costs;
		var destTeleporterTypeCount = reader.ReadInt16();
		var destTeleporterType = new sbyte[destTeleporterTypeCount];
		for (var i = 0; i < destTeleporterTypeCount; i++)
		{
			destTeleporterType[i] = reader.ReadInt8();
		}
		DestTeleporterType = destTeleporterType;
	}
}

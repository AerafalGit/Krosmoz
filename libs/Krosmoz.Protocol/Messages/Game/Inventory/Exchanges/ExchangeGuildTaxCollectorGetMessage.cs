// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeGuildTaxCollectorGetMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5762;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeGuildTaxCollectorGetMessage Empty =>
		new() { CollectorName = string.Empty, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0, UserName = string.Empty, Experience = 0, ObjectsInfos = [] };

	public required string CollectorName { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required ushort SubAreaId { get; set; }

	public required string UserName { get; set; }

	public required double Experience { get; set; }

	public required IEnumerable<ObjectItemGenericQuantity> ObjectsInfos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(CollectorName);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(MapId);
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteUtfPrefixedLength16(UserName);
		writer.WriteDouble(Experience);
		var objectsInfosBefore = writer.Position;
		var objectsInfosCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ObjectsInfos)
		{
			item.Serialize(writer);
			objectsInfosCount++;
		}
		var objectsInfosAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectsInfosBefore);
		writer.WriteInt16((short)objectsInfosCount);
		writer.Seek(SeekOrigin.Begin, objectsInfosAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CollectorName = reader.ReadUtfPrefixedLength16();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		MapId = reader.ReadInt32();
		SubAreaId = reader.ReadVarUInt16();
		UserName = reader.ReadUtfPrefixedLength16();
		Experience = reader.ReadDouble();
		var objectsInfosCount = reader.ReadInt16();
		var objectsInfos = new ObjectItemGenericQuantity[objectsInfosCount];
		for (var i = 0; i < objectsInfosCount; i++)
		{
			var entry = ObjectItemGenericQuantity.Empty;
			entry.Deserialize(reader);
			objectsInfos[i] = entry;
		}
		ObjectsInfos = objectsInfos;
	}
}

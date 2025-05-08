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

	public required short SubAreaId { get; set; }

	public required string UserName { get; set; }

	public required double Experience { get; set; }

	public required IEnumerable<ObjectItemQuantity> ObjectsInfos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfLengthPrefixed16(CollectorName);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteInt(MapId);
		writer.WriteShort(SubAreaId);
		writer.WriteUtfLengthPrefixed16(UserName);
		writer.WriteDouble(Experience);
		var objectsInfosBefore = writer.Position;
		var objectsInfosCount = 0;
		writer.WriteShort(0);
		foreach (var item in ObjectsInfos)
		{
			item.Serialize(writer);
			objectsInfosCount++;
		}
		var objectsInfosAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectsInfosBefore);
		writer.WriteShort((short)objectsInfosCount);
		writer.Seek(SeekOrigin.Begin, objectsInfosAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CollectorName = reader.ReadUtfLengthPrefixed16();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		MapId = reader.ReadInt();
		SubAreaId = reader.ReadShort();
		UserName = reader.ReadUtfLengthPrefixed16();
		Experience = reader.ReadDouble();
		var objectsInfosCount = reader.ReadShort();
		var objectsInfos = new ObjectItemQuantity[objectsInfosCount];
		for (var i = 0; i < objectsInfosCount; i++)
		{
			var entry = ObjectItemQuantity.Empty;
			entry.Deserialize(reader);
			objectsInfos[i] = entry;
		}
		ObjectsInfos = objectsInfos;
	}
}

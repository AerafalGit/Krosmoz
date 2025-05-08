// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Look;

public sealed class EntityLook : DofusType
{
	public new const ushort StaticProtocolId = 55;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static EntityLook Empty =>
		new() { BonesId = 0, Skins = [], IndexedColors = [], Scales = [], Subentities = [] };

	public required short BonesId { get; set; }

	public required IEnumerable<short> Skins { get; set; }

	public required IEnumerable<int> IndexedColors { get; set; }

	public required IEnumerable<short> Scales { get; set; }

	public required IEnumerable<SubEntity> Subentities { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(BonesId);
		var skinsBefore = writer.Position;
		var skinsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Skins)
		{
			writer.WriteShort(item);
			skinsCount++;
		}
		var skinsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, skinsBefore);
		writer.WriteShort((short)skinsCount);
		writer.Seek(SeekOrigin.Begin, skinsAfter);
		var indexedColorsBefore = writer.Position;
		var indexedColorsCount = 0;
		writer.WriteShort(0);
		foreach (var item in IndexedColors)
		{
			writer.WriteInt(item);
			indexedColorsCount++;
		}
		var indexedColorsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, indexedColorsBefore);
		writer.WriteShort((short)indexedColorsCount);
		writer.Seek(SeekOrigin.Begin, indexedColorsAfter);
		var scalesBefore = writer.Position;
		var scalesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Scales)
		{
			writer.WriteShort(item);
			scalesCount++;
		}
		var scalesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, scalesBefore);
		writer.WriteShort((short)scalesCount);
		writer.Seek(SeekOrigin.Begin, scalesAfter);
		var subentitiesBefore = writer.Position;
		var subentitiesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Subentities)
		{
			item.Serialize(writer);
			subentitiesCount++;
		}
		var subentitiesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, subentitiesBefore);
		writer.WriteShort((short)subentitiesCount);
		writer.Seek(SeekOrigin.Begin, subentitiesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		BonesId = reader.ReadShort();
		var skinsCount = reader.ReadShort();
		var skins = new short[skinsCount];
		for (var i = 0; i < skinsCount; i++)
		{
			skins[i] = reader.ReadShort();
		}
		Skins = skins;
		var indexedColorsCount = reader.ReadShort();
		var indexedColors = new int[indexedColorsCount];
		for (var i = 0; i < indexedColorsCount; i++)
		{
			indexedColors[i] = reader.ReadInt();
		}
		IndexedColors = indexedColors;
		var scalesCount = reader.ReadShort();
		var scales = new short[scalesCount];
		for (var i = 0; i < scalesCount; i++)
		{
			scales[i] = reader.ReadShort();
		}
		Scales = scales;
		var subentitiesCount = reader.ReadShort();
		var subentities = new SubEntity[subentitiesCount];
		for (var i = 0; i < subentitiesCount; i++)
		{
			var entry = SubEntity.Empty;
			entry.Deserialize(reader);
			subentities[i] = entry;
		}
		Subentities = subentities;
	}
}

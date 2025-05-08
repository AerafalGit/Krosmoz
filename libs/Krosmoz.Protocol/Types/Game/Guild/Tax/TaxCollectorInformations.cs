// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Guild.Tax;

public sealed class TaxCollectorInformations : DofusType
{
	public new const ushort StaticProtocolId = 167;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static TaxCollectorInformations Empty =>
		new() { UniqueId = 0, FirtNameId = 0, LastNameId = 0, AdditionalInfos = AdditionalTaxCollectorInformations.Empty, WorldX = 0, WorldY = 0, SubAreaId = 0, State = 0, Look = EntityLook.Empty, Complements = [] };

	public required int UniqueId { get; set; }

	public required short FirtNameId { get; set; }

	public required short LastNameId { get; set; }

	public required AdditionalTaxCollectorInformations AdditionalInfos { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required short SubAreaId { get; set; }

	public required sbyte State { get; set; }

	public required EntityLook Look { get; set; }

	public required IEnumerable<TaxCollectorComplementaryInformations> Complements { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(UniqueId);
		writer.WriteShort(FirtNameId);
		writer.WriteShort(LastNameId);
		AdditionalInfos.Serialize(writer);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteShort(SubAreaId);
		writer.WriteSByte(State);
		Look.Serialize(writer);
		var complementsBefore = writer.Position;
		var complementsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Complements)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			complementsCount++;
		}
		var complementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, complementsBefore);
		writer.WriteShort((short)complementsCount);
		writer.Seek(SeekOrigin.Begin, complementsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		UniqueId = reader.ReadInt();
		FirtNameId = reader.ReadShort();
		LastNameId = reader.ReadShort();
		AdditionalInfos = AdditionalTaxCollectorInformations.Empty;
		AdditionalInfos.Deserialize(reader);
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		SubAreaId = reader.ReadShort();
		State = reader.ReadSByte();
		Look = EntityLook.Empty;
		Look.Deserialize(reader);
		var complementsCount = reader.ReadShort();
		var complements = new TaxCollectorComplementaryInformations[complementsCount];
		for (var i = 0; i < complementsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<TaxCollectorComplementaryInformations>(reader.ReadUShort());
			entry.Deserialize(reader);
			complements[i] = entry;
		}
		Complements = complements;
	}
}

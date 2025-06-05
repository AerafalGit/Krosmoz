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

	public required ushort FirtNameId { get; set; }

	public required ushort LastNameId { get; set; }

	public required AdditionalTaxCollectorInformations AdditionalInfos { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required ushort SubAreaId { get; set; }

	public required sbyte State { get; set; }

	public required EntityLook Look { get; set; }

	public required IEnumerable<TaxCollectorComplementaryInformations> Complements { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(UniqueId);
		writer.WriteVarUInt16(FirtNameId);
		writer.WriteVarUInt16(LastNameId);
		AdditionalInfos.Serialize(writer);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteInt8(State);
		Look.Serialize(writer);
		var complementsBefore = writer.Position;
		var complementsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Complements)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			complementsCount++;
		}
		var complementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, complementsBefore);
		writer.WriteInt16((short)complementsCount);
		writer.Seek(SeekOrigin.Begin, complementsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		UniqueId = reader.ReadInt32();
		FirtNameId = reader.ReadVarUInt16();
		LastNameId = reader.ReadVarUInt16();
		AdditionalInfos = AdditionalTaxCollectorInformations.Empty;
		AdditionalInfos.Deserialize(reader);
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		SubAreaId = reader.ReadVarUInt16();
		State = reader.ReadInt8();
		Look = EntityLook.Empty;
		Look.Deserialize(reader);
		var complementsCount = reader.ReadInt16();
		var complements = new TaxCollectorComplementaryInformations[complementsCount];
		for (var i = 0; i < complementsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<TaxCollectorComplementaryInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			complements[i] = entry;
		}
		Complements = complements;
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class SellerBuyerDescriptor : DofusType
{
	public new const ushort StaticProtocolId = 121;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static SellerBuyerDescriptor Empty =>
		new() { Quantities = [], Types = [], TaxPercentage = 0, TaxModificationPercentage = 0, MaxItemLevel = 0, MaxItemPerAccount = 0, NpcContextualId = 0, UnsoldDelay = 0 };

	public required IEnumerable<uint> Quantities { get; set; }

	public required IEnumerable<uint> Types { get; set; }

	public required float TaxPercentage { get; set; }

	public required float TaxModificationPercentage { get; set; }

	public required byte MaxItemLevel { get; set; }

	public required uint MaxItemPerAccount { get; set; }

	public required int NpcContextualId { get; set; }

	public required ushort UnsoldDelay { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var quantitiesBefore = writer.Position;
		var quantitiesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Quantities)
		{
			writer.WriteVarUInt32(item);
			quantitiesCount++;
		}
		var quantitiesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, quantitiesBefore);
		writer.WriteInt16((short)quantitiesCount);
		writer.Seek(SeekOrigin.Begin, quantitiesAfter);
		var typesBefore = writer.Position;
		var typesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Types)
		{
			writer.WriteVarUInt32(item);
			typesCount++;
		}
		var typesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, typesBefore);
		writer.WriteInt16((short)typesCount);
		writer.Seek(SeekOrigin.Begin, typesAfter);
		writer.WriteSingle(TaxPercentage);
		writer.WriteSingle(TaxModificationPercentage);
		writer.WriteUInt8(MaxItemLevel);
		writer.WriteVarUInt32(MaxItemPerAccount);
		writer.WriteInt32(NpcContextualId);
		writer.WriteVarUInt16(UnsoldDelay);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var quantitiesCount = reader.ReadInt16();
		var quantities = new uint[quantitiesCount];
		for (var i = 0; i < quantitiesCount; i++)
		{
			quantities[i] = reader.ReadVarUInt32();
		}
		Quantities = quantities;
		var typesCount = reader.ReadInt16();
		var types = new uint[typesCount];
		for (var i = 0; i < typesCount; i++)
		{
			types[i] = reader.ReadVarUInt32();
		}
		Types = types;
		TaxPercentage = reader.ReadSingle();
		TaxModificationPercentage = reader.ReadSingle();
		MaxItemLevel = reader.ReadUInt8();
		MaxItemPerAccount = reader.ReadVarUInt32();
		NpcContextualId = reader.ReadInt32();
		UnsoldDelay = reader.ReadVarUInt16();
	}
}

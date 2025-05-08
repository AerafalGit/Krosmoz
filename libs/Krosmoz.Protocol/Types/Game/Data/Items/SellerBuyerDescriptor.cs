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
		new() { Quantities = [], Types = [], TaxPercentage = 0, MaxItemLevel = 0, MaxItemPerAccount = 0, NpcContextualId = 0, UnsoldDelay = 0 };

	public required IEnumerable<int> Quantities { get; set; }

	public required IEnumerable<int> Types { get; set; }

	public required float TaxPercentage { get; set; }

	public required int MaxItemLevel { get; set; }

	public required int MaxItemPerAccount { get; set; }

	public required int NpcContextualId { get; set; }

	public required short UnsoldDelay { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var quantitiesBefore = writer.Position;
		var quantitiesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Quantities)
		{
			writer.WriteInt(item);
			quantitiesCount++;
		}
		var quantitiesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, quantitiesBefore);
		writer.WriteShort((short)quantitiesCount);
		writer.Seek(SeekOrigin.Begin, quantitiesAfter);
		var typesBefore = writer.Position;
		var typesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Types)
		{
			writer.WriteInt(item);
			typesCount++;
		}
		var typesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, typesBefore);
		writer.WriteShort((short)typesCount);
		writer.Seek(SeekOrigin.Begin, typesAfter);
		writer.WriteSingle(TaxPercentage);
		writer.WriteInt(MaxItemLevel);
		writer.WriteInt(MaxItemPerAccount);
		writer.WriteInt(NpcContextualId);
		writer.WriteShort(UnsoldDelay);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var quantitiesCount = reader.ReadShort();
		var quantities = new int[quantitiesCount];
		for (var i = 0; i < quantitiesCount; i++)
		{
			quantities[i] = reader.ReadInt();
		}
		Quantities = quantities;
		var typesCount = reader.ReadShort();
		var types = new int[typesCount];
		for (var i = 0; i < typesCount; i++)
		{
			types[i] = reader.ReadInt();
		}
		Types = types;
		TaxPercentage = reader.ReadSingle();
		MaxItemLevel = reader.ReadInt();
		MaxItemPerAccount = reader.ReadInt();
		NpcContextualId = reader.ReadInt();
		UnsoldDelay = reader.ReadShort();
	}
}

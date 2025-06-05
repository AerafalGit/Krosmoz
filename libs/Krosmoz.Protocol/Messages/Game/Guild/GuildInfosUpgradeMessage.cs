// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildInfosUpgradeMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5636;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildInfosUpgradeMessage Empty =>
		new() { MaxTaxCollectorsCount = 0, TaxCollectorsCount = 0, TaxCollectorLifePoints = 0, TaxCollectorDamagesBonuses = 0, TaxCollectorPods = 0, TaxCollectorProspecting = 0, TaxCollectorWisdom = 0, BoostPoints = 0, SpellId = [], SpellLevel = [] };

	public required sbyte MaxTaxCollectorsCount { get; set; }

	public required sbyte TaxCollectorsCount { get; set; }

	public required ushort TaxCollectorLifePoints { get; set; }

	public required ushort TaxCollectorDamagesBonuses { get; set; }

	public required ushort TaxCollectorPods { get; set; }

	public required ushort TaxCollectorProspecting { get; set; }

	public required ushort TaxCollectorWisdom { get; set; }

	public required ushort BoostPoints { get; set; }

	public required IEnumerable<ushort> SpellId { get; set; }

	public required IEnumerable<sbyte> SpellLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(MaxTaxCollectorsCount);
		writer.WriteInt8(TaxCollectorsCount);
		writer.WriteVarUInt16(TaxCollectorLifePoints);
		writer.WriteVarUInt16(TaxCollectorDamagesBonuses);
		writer.WriteVarUInt16(TaxCollectorPods);
		writer.WriteVarUInt16(TaxCollectorProspecting);
		writer.WriteVarUInt16(TaxCollectorWisdom);
		writer.WriteVarUInt16(BoostPoints);
		var spellIdBefore = writer.Position;
		var spellIdCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SpellId)
		{
			writer.WriteVarUInt16(item);
			spellIdCount++;
		}
		var spellIdAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, spellIdBefore);
		writer.WriteInt16((short)spellIdCount);
		writer.Seek(SeekOrigin.Begin, spellIdAfter);
		var spellLevelBefore = writer.Position;
		var spellLevelCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SpellLevel)
		{
			writer.WriteInt8(item);
			spellLevelCount++;
		}
		var spellLevelAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, spellLevelBefore);
		writer.WriteInt16((short)spellLevelCount);
		writer.Seek(SeekOrigin.Begin, spellLevelAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MaxTaxCollectorsCount = reader.ReadInt8();
		TaxCollectorsCount = reader.ReadInt8();
		TaxCollectorLifePoints = reader.ReadVarUInt16();
		TaxCollectorDamagesBonuses = reader.ReadVarUInt16();
		TaxCollectorPods = reader.ReadVarUInt16();
		TaxCollectorProspecting = reader.ReadVarUInt16();
		TaxCollectorWisdom = reader.ReadVarUInt16();
		BoostPoints = reader.ReadVarUInt16();
		var spellIdCount = reader.ReadInt16();
		var spellId = new ushort[spellIdCount];
		for (var i = 0; i < spellIdCount; i++)
		{
			spellId[i] = reader.ReadVarUInt16();
		}
		SpellId = spellId;
		var spellLevelCount = reader.ReadInt16();
		var spellLevel = new sbyte[spellLevelCount];
		for (var i = 0; i < spellLevelCount; i++)
		{
			spellLevel[i] = reader.ReadInt8();
		}
		SpellLevel = spellLevel;
	}
}

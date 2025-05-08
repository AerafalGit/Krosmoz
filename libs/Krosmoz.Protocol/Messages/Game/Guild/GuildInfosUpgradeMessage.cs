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

	public required short TaxCollectorLifePoints { get; set; }

	public required short TaxCollectorDamagesBonuses { get; set; }

	public required short TaxCollectorPods { get; set; }

	public required short TaxCollectorProspecting { get; set; }

	public required short TaxCollectorWisdom { get; set; }

	public required short BoostPoints { get; set; }

	public required IEnumerable<short> SpellId { get; set; }

	public required IEnumerable<sbyte> SpellLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(MaxTaxCollectorsCount);
		writer.WriteSByte(TaxCollectorsCount);
		writer.WriteShort(TaxCollectorLifePoints);
		writer.WriteShort(TaxCollectorDamagesBonuses);
		writer.WriteShort(TaxCollectorPods);
		writer.WriteShort(TaxCollectorProspecting);
		writer.WriteShort(TaxCollectorWisdom);
		writer.WriteShort(BoostPoints);
		var spellIdBefore = writer.Position;
		var spellIdCount = 0;
		writer.WriteShort(0);
		foreach (var item in SpellId)
		{
			writer.WriteShort(item);
			spellIdCount++;
		}
		var spellIdAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, spellIdBefore);
		writer.WriteShort((short)spellIdCount);
		writer.Seek(SeekOrigin.Begin, spellIdAfter);
		var spellLevelBefore = writer.Position;
		var spellLevelCount = 0;
		writer.WriteShort(0);
		foreach (var item in SpellLevel)
		{
			writer.WriteSByte(item);
			spellLevelCount++;
		}
		var spellLevelAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, spellLevelBefore);
		writer.WriteShort((short)spellLevelCount);
		writer.Seek(SeekOrigin.Begin, spellLevelAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MaxTaxCollectorsCount = reader.ReadSByte();
		TaxCollectorsCount = reader.ReadSByte();
		TaxCollectorLifePoints = reader.ReadShort();
		TaxCollectorDamagesBonuses = reader.ReadShort();
		TaxCollectorPods = reader.ReadShort();
		TaxCollectorProspecting = reader.ReadShort();
		TaxCollectorWisdom = reader.ReadShort();
		BoostPoints = reader.ReadShort();
		var spellIdCount = reader.ReadShort();
		var spellId = new short[spellIdCount];
		for (var i = 0; i < spellIdCount; i++)
		{
			spellId[i] = reader.ReadShort();
		}
		SpellId = spellId;
		var spellLevelCount = reader.ReadShort();
		var spellLevel = new sbyte[spellLevelCount];
		for (var i = 0; i < spellLevelCount; i++)
		{
			spellLevel[i] = reader.ReadSByte();
		}
		SpellLevel = spellLevel;
	}
}

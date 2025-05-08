// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Characteristic;
using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Spells;

public sealed class SlaveSwitchContextMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6214;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SlaveSwitchContextMessage Empty =>
		new() { SummonerId = 0, SlaveId = 0, SlaveSpells = [], SlaveStats = CharacterCharacteristicsInformations.Empty };

	public required int SummonerId { get; set; }

	public required int SlaveId { get; set; }

	public required IEnumerable<SpellItem> SlaveSpells { get; set; }

	public required CharacterCharacteristicsInformations SlaveStats { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(SummonerId);
		writer.WriteInt(SlaveId);
		var slaveSpellsBefore = writer.Position;
		var slaveSpellsCount = 0;
		writer.WriteShort(0);
		foreach (var item in SlaveSpells)
		{
			item.Serialize(writer);
			slaveSpellsCount++;
		}
		var slaveSpellsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, slaveSpellsBefore);
		writer.WriteShort((short)slaveSpellsCount);
		writer.Seek(SeekOrigin.Begin, slaveSpellsAfter);
		SlaveStats.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SummonerId = reader.ReadInt();
		SlaveId = reader.ReadInt();
		var slaveSpellsCount = reader.ReadShort();
		var slaveSpells = new SpellItem[slaveSpellsCount];
		for (var i = 0; i < slaveSpellsCount; i++)
		{
			var entry = SpellItem.Empty;
			entry.Deserialize(reader);
			slaveSpells[i] = entry;
		}
		SlaveSpells = slaveSpells;
		SlaveStats = CharacterCharacteristicsInformations.Empty;
		SlaveStats.Deserialize(reader);
	}
}

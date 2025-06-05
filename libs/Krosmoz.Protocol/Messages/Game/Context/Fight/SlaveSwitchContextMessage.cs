// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Characteristic;
using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class SlaveSwitchContextMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6214;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SlaveSwitchContextMessage Empty =>
		new() { MasterId = 0, SlaveId = 0, SlaveSpells = [], SlaveStats = CharacterCharacteristicsInformations.Empty, Shortcuts = [] };

	public required int MasterId { get; set; }

	public required int SlaveId { get; set; }

	public required IEnumerable<SpellItem> SlaveSpells { get; set; }

	public required CharacterCharacteristicsInformations SlaveStats { get; set; }

	public required IEnumerable<Types.Game.Shortcut.Shortcut> Shortcuts { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(MasterId);
		writer.WriteInt32(SlaveId);
		var slaveSpellsBefore = writer.Position;
		var slaveSpellsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SlaveSpells)
		{
			item.Serialize(writer);
			slaveSpellsCount++;
		}
		var slaveSpellsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, slaveSpellsBefore);
		writer.WriteInt16((short)slaveSpellsCount);
		writer.Seek(SeekOrigin.Begin, slaveSpellsAfter);
		SlaveStats.Serialize(writer);
		var shortcutsBefore = writer.Position;
		var shortcutsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Shortcuts)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			shortcutsCount++;
		}
		var shortcutsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, shortcutsBefore);
		writer.WriteInt16((short)shortcutsCount);
		writer.Seek(SeekOrigin.Begin, shortcutsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MasterId = reader.ReadInt32();
		SlaveId = reader.ReadInt32();
		var slaveSpellsCount = reader.ReadInt16();
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
		var shortcutsCount = reader.ReadInt16();
		var shortcuts = new Types.Game.Shortcut.Shortcut[shortcutsCount];
		for (var i = 0; i < shortcutsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<Types.Game.Shortcut.Shortcut>(reader.ReadUInt16());
			entry.Deserialize(reader);
			shortcuts[i] = entry;
		}
		Shortcuts = shortcuts;
	}
}

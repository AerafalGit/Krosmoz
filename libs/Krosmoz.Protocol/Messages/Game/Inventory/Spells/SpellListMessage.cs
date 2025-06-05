// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Spells;

public sealed class SpellListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1200;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SpellListMessage Empty =>
		new() { SpellPrevisualization = false, Spells = [] };

	public required bool SpellPrevisualization { get; set; }

	public required IEnumerable<SpellItem> Spells { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(SpellPrevisualization);
		var spellsBefore = writer.Position;
		var spellsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Spells)
		{
			item.Serialize(writer);
			spellsCount++;
		}
		var spellsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, spellsBefore);
		writer.WriteInt16((short)spellsCount);
		writer.Seek(SeekOrigin.Begin, spellsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpellPrevisualization = reader.ReadBoolean();
		var spellsCount = reader.ReadInt16();
		var spells = new SpellItem[spellsCount];
		for (var i = 0; i < spellsCount; i++)
		{
			var entry = SpellItem.Empty;
			entry.Deserialize(reader);
			spells[i] = entry;
		}
		Spells = spells;
	}
}

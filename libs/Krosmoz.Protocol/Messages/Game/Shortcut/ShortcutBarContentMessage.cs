// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Shortcut;

public sealed class ShortcutBarContentMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6231;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ShortcutBarContentMessage Empty =>
		new() { BarType = 0, Shortcuts = [] };

	public required sbyte BarType { get; set; }

	public required IEnumerable<Types.Game.Shortcut.Shortcut> Shortcuts { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(BarType);
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
		BarType = reader.ReadInt8();
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

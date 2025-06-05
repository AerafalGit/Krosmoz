// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Shortcut;

public sealed class ShortcutSpell : Shortcut
{
	public new const ushort StaticProtocolId = 368;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ShortcutSpell Empty =>
		new() { Slot = 0, SpellId = 0 };

	public required ushort SpellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(SpellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SpellId = reader.ReadVarUInt16();
	}
}

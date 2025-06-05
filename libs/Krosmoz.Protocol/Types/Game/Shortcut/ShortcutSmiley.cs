// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Shortcut;

public sealed class ShortcutSmiley : Shortcut
{
	public new const ushort StaticProtocolId = 388;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ShortcutSmiley Empty =>
		new() { Slot = 0, SmileyId = 0 };

	public required sbyte SmileyId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(SmileyId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SmileyId = reader.ReadInt8();
	}
}

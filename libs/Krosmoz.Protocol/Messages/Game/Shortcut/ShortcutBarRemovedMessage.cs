// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Shortcut;

public sealed class ShortcutBarRemovedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6224;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ShortcutBarRemovedMessage Empty =>
		new() { BarType = 0, Slot = 0 };

	public required sbyte BarType { get; set; }

	public required sbyte Slot { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(BarType);
		writer.WriteInt8(Slot);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		BarType = reader.ReadInt8();
		Slot = reader.ReadInt8();
	}
}

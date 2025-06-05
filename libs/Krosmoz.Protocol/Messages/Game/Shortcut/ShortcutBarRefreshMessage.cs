// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Shortcut;

public sealed class ShortcutBarRefreshMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6229;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ShortcutBarRefreshMessage Empty =>
		new() { BarType = 0, Shortcut = Types.Game.Shortcut.Shortcut.Empty };

	public required sbyte BarType { get; set; }

	public required Types.Game.Shortcut.Shortcut Shortcut { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(BarType);
		writer.WriteUInt16(Shortcut.ProtocolId);
		Shortcut.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		BarType = reader.ReadInt8();
		Shortcut = Types.TypeFactory.CreateType<Types.Game.Shortcut.Shortcut>(reader.ReadUInt16());
		Shortcut.Deserialize(reader);
	}
}

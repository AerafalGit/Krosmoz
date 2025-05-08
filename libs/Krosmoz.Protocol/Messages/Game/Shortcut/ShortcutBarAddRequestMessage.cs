// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Shortcut;

public sealed class ShortcutBarAddRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6225;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ShortcutBarAddRequestMessage Empty =>
		new() { BarType = 0, Shortcut = Types.Game.Shortcut.Shortcut.Empty };

	public required sbyte BarType { get; set; }

	public required Types.Game.Shortcut.Shortcut Shortcut { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(BarType);
		writer.WriteUShort(Shortcut.ProtocolId);
		Shortcut.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		BarType = reader.ReadSByte();
		Shortcut = Types.TypeFactory.CreateType<Types.Game.Shortcut.Shortcut>(reader.ReadUShort());
		Shortcut.Deserialize(reader);
	}
}

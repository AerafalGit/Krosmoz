// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Shortcut;

public sealed class ShortcutBarSwapRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6230;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ShortcutBarSwapRequestMessage Empty =>
		new() { BarType = 0, FirstSlot = 0, SecondSlot = 0 };

	public required sbyte BarType { get; set; }

	public required int FirstSlot { get; set; }

	public required int SecondSlot { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(BarType);
		writer.WriteInt(FirstSlot);
		writer.WriteInt(SecondSlot);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		BarType = reader.ReadSByte();
		FirstSlot = reader.ReadInt();
		SecondSlot = reader.ReadInt();
	}
}

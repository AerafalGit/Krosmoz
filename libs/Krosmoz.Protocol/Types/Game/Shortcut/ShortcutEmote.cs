// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Shortcut;

public sealed class ShortcutEmote : Shortcut
{
	public new const ushort StaticProtocolId = 389;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ShortcutEmote Empty =>
		new() { Slot = 0, EmoteId = 0 };

	public required byte EmoteId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt8(EmoteId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		EmoteId = reader.ReadUInt8();
	}
}

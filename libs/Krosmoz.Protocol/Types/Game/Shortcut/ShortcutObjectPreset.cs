// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Shortcut;

public sealed class ShortcutObjectPreset : ShortcutObject
{
	public new const ushort StaticProtocolId = 370;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ShortcutObjectPreset Empty =>
		new() { Slot = 0, PresetId = 0 };

	public required sbyte PresetId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(PresetId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PresetId = reader.ReadInt8();
	}
}

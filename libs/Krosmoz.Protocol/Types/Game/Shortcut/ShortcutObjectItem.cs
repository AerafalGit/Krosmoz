// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Shortcut;

public sealed class ShortcutObjectItem : ShortcutObject
{
	public new const ushort StaticProtocolId = 371;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ShortcutObjectItem Empty =>
		new() { Slot = 0, ItemUID = 0, ItemGID = 0 };

	public required int ItemUID { get; set; }

	public required int ItemGID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(ItemUID);
		writer.WriteInt32(ItemGID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ItemUID = reader.ReadInt32();
		ItemGID = reader.ReadInt32();
	}
}

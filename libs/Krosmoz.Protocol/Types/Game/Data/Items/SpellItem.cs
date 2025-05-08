// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class SpellItem : Item
{
	public new const ushort StaticProtocolId = 49;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new SpellItem Empty =>
		new() { Position = 0, SpellId = 0, SpellLevel = 0 };

	public required byte Position { get; set; }

	public required int SpellId { get; set; }

	public required sbyte SpellLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(Position);
		writer.WriteInt(SpellId);
		writer.WriteSByte(SpellLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Position = reader.ReadByte();
		SpellId = reader.ReadInt();
		SpellLevel = reader.ReadSByte();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectSetPositionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3021;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectSetPositionMessage Empty =>
		new() { ObjectUID = 0, Position = 0, Quantity = 0 };

	public required uint ObjectUID { get; set; }

	public required byte Position { get; set; }

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ObjectUID);
		writer.WriteUInt8(Position);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectUID = reader.ReadVarUInt32();
		Position = reader.ReadUInt8();
		Quantity = reader.ReadVarUInt32();
	}
}

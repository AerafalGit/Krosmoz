// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectMovementMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3010;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectMovementMessage Empty =>
		new() { ObjectUID = 0, Position = 0 };

	public required uint ObjectUID { get; set; }

	public required byte Position { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ObjectUID);
		writer.WriteUInt8(Position);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectUID = reader.ReadVarUInt32();
		Position = reader.ReadUInt8();
	}
}

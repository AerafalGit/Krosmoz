// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class ObjectItemQuantity : Item
{
	public new const ushort StaticProtocolId = 119;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectItemQuantity Empty =>
		new() { ObjectUID = 0, Quantity = 0 };

	public required uint ObjectUID { get; set; }

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(ObjectUID);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ObjectUID = reader.ReadVarUInt32();
		Quantity = reader.ReadVarUInt32();
	}
}

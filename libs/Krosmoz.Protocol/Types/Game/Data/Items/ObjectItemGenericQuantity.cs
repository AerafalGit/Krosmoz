// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class ObjectItemGenericQuantity : Item
{
	public new const ushort StaticProtocolId = 483;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectItemGenericQuantity Empty =>
		new() { ObjectGID = 0, Quantity = 0 };

	public required ushort ObjectGID { get; set; }

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(ObjectGID);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ObjectGID = reader.ReadVarUInt16();
		Quantity = reader.ReadVarUInt32();
	}
}

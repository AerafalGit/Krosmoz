// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectDropMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3005;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectDropMessage Empty =>
		new() { ObjectUID = 0, Quantity = 0 };

	public required uint ObjectUID { get; set; }

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ObjectUID);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectUID = reader.ReadVarUInt32();
		Quantity = reader.ReadVarUInt32();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectUseMultipleMessage : ObjectUseMessage
{
	public new const uint StaticProtocolId = 6234;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ObjectUseMultipleMessage Empty =>
		new() { ObjectUID = 0, Quantity = 0 };

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Quantity = reader.ReadVarUInt32();
	}
}

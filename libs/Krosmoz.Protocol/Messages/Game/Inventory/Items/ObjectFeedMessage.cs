// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectFeedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6290;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectFeedMessage Empty =>
		new() { ObjectUID = 0, FoodUID = 0, FoodQuantity = 0 };

	public required uint ObjectUID { get; set; }

	public required uint FoodUID { get; set; }

	public required uint FoodQuantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ObjectUID);
		writer.WriteVarUInt32(FoodUID);
		writer.WriteVarUInt32(FoodQuantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectUID = reader.ReadVarUInt32();
		FoodUID = reader.ReadVarUInt32();
		FoodQuantity = reader.ReadVarUInt32();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObtainedItemWithBonusMessage : ObtainedItemMessage
{
	public new const uint StaticProtocolId = 6520;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ObtainedItemWithBonusMessage Empty =>
		new() { BaseQuantity = 0, GenericId = 0, BonusQuantity = 0 };

	public required uint BonusQuantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(BonusQuantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		BonusQuantity = reader.ReadVarUInt32();
	}
}

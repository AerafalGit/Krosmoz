// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public class ObtainedItemMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6519;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObtainedItemMessage Empty =>
		new() { GenericId = 0, BaseQuantity = 0 };

	public required ushort GenericId { get; set; }

	public required uint BaseQuantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(GenericId);
		writer.WriteVarUInt32(BaseQuantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GenericId = reader.ReadVarUInt16();
		BaseQuantity = reader.ReadVarUInt32();
	}
}

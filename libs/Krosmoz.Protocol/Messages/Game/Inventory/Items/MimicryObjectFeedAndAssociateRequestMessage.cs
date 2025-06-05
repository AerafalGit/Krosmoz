// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class MimicryObjectFeedAndAssociateRequestMessage : SymbioticObjectAssociateRequestMessage
{
	public new const uint StaticProtocolId = 6460;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new MimicryObjectFeedAndAssociateRequestMessage Empty =>
		new() { HostPos = 0, HostUID = 0, SymbiotePos = 0, SymbioteUID = 0, FoodUID = 0, FoodPos = 0, Preview = false };

	public required uint FoodUID { get; set; }

	public required byte FoodPos { get; set; }

	public required bool Preview { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(FoodUID);
		writer.WriteUInt8(FoodPos);
		writer.WriteBoolean(Preview);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		FoodUID = reader.ReadVarUInt32();
		FoodPos = reader.ReadUInt8();
		Preview = reader.ReadBoolean();
	}
}

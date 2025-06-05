// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class LivingObjectChangeSkinRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5725;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static LivingObjectChangeSkinRequestMessage Empty =>
		new() { LivingUID = 0, LivingPosition = 0, SkinId = 0 };

	public required uint LivingUID { get; set; }

	public required byte LivingPosition { get; set; }

	public required uint SkinId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(LivingUID);
		writer.WriteUInt8(LivingPosition);
		writer.WriteVarUInt32(SkinId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		LivingUID = reader.ReadVarUInt32();
		LivingPosition = reader.ReadUInt8();
		SkinId = reader.ReadVarUInt32();
	}
}

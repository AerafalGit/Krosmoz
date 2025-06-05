// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public sealed class HouseToSellFilterMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6137;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseToSellFilterMessage Empty =>
		new() { AreaId = 0, AtLeastNbRoom = 0, AtLeastNbChest = 0, SkillRequested = 0, MaxPrice = 0 };

	public required int AreaId { get; set; }

	public required sbyte AtLeastNbRoom { get; set; }

	public required sbyte AtLeastNbChest { get; set; }

	public required ushort SkillRequested { get; set; }

	public required uint MaxPrice { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(AreaId);
		writer.WriteInt8(AtLeastNbRoom);
		writer.WriteInt8(AtLeastNbChest);
		writer.WriteVarUInt16(SkillRequested);
		writer.WriteVarUInt32(MaxPrice);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AreaId = reader.ReadInt32();
		AtLeastNbRoom = reader.ReadInt8();
		AtLeastNbChest = reader.ReadInt8();
		SkillRequested = reader.ReadVarUInt16();
		MaxPrice = reader.ReadVarUInt32();
	}
}

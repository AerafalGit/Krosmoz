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

	public required short SkillRequested { get; set; }

	public required int MaxPrice { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(AreaId);
		writer.WriteSByte(AtLeastNbRoom);
		writer.WriteSByte(AtLeastNbChest);
		writer.WriteShort(SkillRequested);
		writer.WriteInt(MaxPrice);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AreaId = reader.ReadInt();
		AtLeastNbRoom = reader.ReadSByte();
		AtLeastNbChest = reader.ReadSByte();
		SkillRequested = reader.ReadShort();
		MaxPrice = reader.ReadInt();
	}
}

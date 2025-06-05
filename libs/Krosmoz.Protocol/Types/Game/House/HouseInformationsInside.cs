// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.House;

public sealed class HouseInformationsInside : DofusType
{
	public new const ushort StaticProtocolId = 218;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static HouseInformationsInside Empty =>
		new() { HouseId = 0, ModelId = 0, OwnerId = 0, OwnerName = string.Empty, WorldX = 0, WorldY = 0, Price = 0, IsLocked = false };

	public required uint HouseId { get; set; }

	public required ushort ModelId { get; set; }

	public required int OwnerId { get; set; }

	public required string OwnerName { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int Price { get; set; }

	public required bool IsLocked { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(HouseId);
		writer.WriteVarUInt16(ModelId);
		writer.WriteInt32(OwnerId);
		writer.WriteUtfPrefixedLength16(OwnerName);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(Price);
		writer.WriteBoolean(IsLocked);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HouseId = reader.ReadVarUInt32();
		ModelId = reader.ReadVarUInt16();
		OwnerId = reader.ReadInt32();
		OwnerName = reader.ReadUtfPrefixedLength16();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		Price = reader.ReadInt32();
		IsLocked = reader.ReadBoolean();
	}
}

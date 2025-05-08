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

	public required int HouseId { get; set; }

	public required short ModelId { get; set; }

	public required int OwnerId { get; set; }

	public required string OwnerName { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required uint Price { get; set; }

	public required bool IsLocked { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(HouseId);
		writer.WriteShort(ModelId);
		writer.WriteInt(OwnerId);
		writer.WriteUtfLengthPrefixed16(OwnerName);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteUInt(Price);
		writer.WriteBoolean(IsLocked);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HouseId = reader.ReadInt();
		ModelId = reader.ReadShort();
		OwnerId = reader.ReadInt();
		OwnerName = reader.ReadUtfLengthPrefixed16();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		Price = reader.ReadUInt();
		IsLocked = reader.ReadBoolean();
	}
}

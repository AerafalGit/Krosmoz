// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.House;

public sealed class AccountHouseInformations : DofusType
{
	public new const ushort StaticProtocolId = 390;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AccountHouseInformations Empty =>
		new() { HouseId = 0, ModelId = 0, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0 };

	public required uint HouseId { get; set; }

	public required ushort ModelId { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required ushort SubAreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(HouseId);
		writer.WriteVarUInt16(ModelId);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(MapId);
		writer.WriteVarUInt16(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HouseId = reader.ReadVarUInt32();
		ModelId = reader.ReadVarUInt16();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		MapId = reader.ReadInt32();
		SubAreaId = reader.ReadVarUInt16();
	}
}

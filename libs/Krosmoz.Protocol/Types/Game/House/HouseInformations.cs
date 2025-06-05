// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.House;

public class HouseInformations : DofusType
{
	public new const ushort StaticProtocolId = 111;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static HouseInformations Empty =>
		new() { IsOnSale = false, IsSaleLocked = false, HouseId = 0, DoorsOnMap = [], OwnerName = string.Empty, ModelId = 0 };

	public required bool IsOnSale { get; set; }

	public required bool IsSaleLocked { get; set; }

	public required uint HouseId { get; set; }

	public required IEnumerable<int> DoorsOnMap { get; set; }

	public required string OwnerName { get; set; }

	public required ushort ModelId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, IsOnSale);
		flag = BooleanByteWrapper.SetFlag(flag, 1, IsSaleLocked);
		writer.WriteUInt8(flag);
		writer.WriteVarUInt32(HouseId);
		var doorsOnMapBefore = writer.Position;
		var doorsOnMapCount = 0;
		writer.WriteInt16(0);
		foreach (var item in DoorsOnMap)
		{
			writer.WriteInt32(item);
			doorsOnMapCount++;
		}
		var doorsOnMapAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, doorsOnMapBefore);
		writer.WriteInt16((short)doorsOnMapCount);
		writer.Seek(SeekOrigin.Begin, doorsOnMapAfter);
		writer.WriteUtfPrefixedLength16(OwnerName);
		writer.WriteVarUInt16(ModelId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		IsOnSale = BooleanByteWrapper.GetFlag(flag, 0);
		IsSaleLocked = BooleanByteWrapper.GetFlag(flag, 1);
		HouseId = reader.ReadVarUInt32();
		var doorsOnMapCount = reader.ReadInt16();
		var doorsOnMap = new int[doorsOnMapCount];
		for (var i = 0; i < doorsOnMapCount; i++)
		{
			doorsOnMap[i] = reader.ReadInt32();
		}
		DoorsOnMap = doorsOnMap;
		OwnerName = reader.ReadUtfPrefixedLength16();
		ModelId = reader.ReadVarUInt16();
	}
}

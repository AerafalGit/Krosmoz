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

	public required int HouseId { get; set; }

	public required IEnumerable<int> DoorsOnMap { get; set; }

	public required string OwnerName { get; set; }

	public required short ModelId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, IsOnSale);
		flag = BooleanByteWrapper.SetFlag(flag, 1, IsSaleLocked);
		writer.WriteByte(flag);
		writer.WriteInt(HouseId);
		var doorsOnMapBefore = writer.Position;
		var doorsOnMapCount = 0;
		writer.WriteShort(0);
		foreach (var item in DoorsOnMap)
		{
			writer.WriteInt(item);
			doorsOnMapCount++;
		}
		var doorsOnMapAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, doorsOnMapBefore);
		writer.WriteShort((short)doorsOnMapCount);
		writer.Seek(SeekOrigin.Begin, doorsOnMapAfter);
		writer.WriteUtfLengthPrefixed16(OwnerName);
		writer.WriteShort(ModelId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadByte();
		IsOnSale = BooleanByteWrapper.GetFlag(flag, 0);
		IsSaleLocked = BooleanByteWrapper.GetFlag(flag, 1);
		HouseId = reader.ReadInt();
		var doorsOnMapCount = reader.ReadShort();
		var doorsOnMap = new int[doorsOnMapCount];
		for (var i = 0; i < doorsOnMapCount; i++)
		{
			doorsOnMap[i] = reader.ReadInt();
		}
		DoorsOnMap = doorsOnMap;
		OwnerName = reader.ReadUtfLengthPrefixed16();
		ModelId = reader.ReadShort();
	}
}

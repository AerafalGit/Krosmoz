// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.House;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public sealed class HouseToSellListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6140;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseToSellListMessage Empty =>
		new() { PageIndex = 0, TotalPage = 0, HouseList = [] };

	public required short PageIndex { get; set; }

	public required short TotalPage { get; set; }

	public required IEnumerable<HouseInformationsForSell> HouseList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(PageIndex);
		writer.WriteInt16(TotalPage);
		var houseListBefore = writer.Position;
		var houseListCount = 0;
		writer.WriteInt16(0);
		foreach (var item in HouseList)
		{
			item.Serialize(writer);
			houseListCount++;
		}
		var houseListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, houseListBefore);
		writer.WriteInt16((short)houseListCount);
		writer.Seek(SeekOrigin.Begin, houseListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PageIndex = reader.ReadInt16();
		TotalPage = reader.ReadInt16();
		var houseListCount = reader.ReadInt16();
		var houseList = new HouseInformationsForSell[houseListCount];
		for (var i = 0; i < houseListCount; i++)
		{
			var entry = HouseInformationsForSell.Empty;
			entry.Deserialize(reader);
			houseList[i] = entry;
		}
		HouseList = houseList;
	}
}

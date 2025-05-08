// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.House;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public sealed class MapComplementaryInformationsDataInHouseMessage : MapComplementaryInformationsDataMessage
{
	public new const uint StaticProtocolId = 6130;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new MapComplementaryInformationsDataInHouseMessage Empty =>
		new() { Fights = [], Obstacles = [], StatedElements = [], InteractiveElements = [], Actors = [], Houses = [], MapId = 0, SubAreaId = 0, CurrentHouse = HouseInformationsInside.Empty };

	public required HouseInformationsInside CurrentHouse { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		CurrentHouse.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CurrentHouse = HouseInformationsInside.Empty;
		CurrentHouse.Deserialize(reader);
	}
}

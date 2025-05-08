// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public sealed class MapComplementaryInformationsWithCoordsMessage : MapComplementaryInformationsDataMessage
{
	public new const uint StaticProtocolId = 6268;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new MapComplementaryInformationsWithCoordsMessage Empty =>
		new() { Fights = [], Obstacles = [], StatedElements = [], InteractiveElements = [], Actors = [], Houses = [], MapId = 0, SubAreaId = 0, WorldX = 0, WorldY = 0 };

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
	}
}

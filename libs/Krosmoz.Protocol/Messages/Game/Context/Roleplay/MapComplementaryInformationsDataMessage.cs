// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;
using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.House;
using Krosmoz.Protocol.Types.Game.Interactive;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public class MapComplementaryInformationsDataMessage : DofusMessage
{
	public new const uint StaticProtocolId = 226;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MapComplementaryInformationsDataMessage Empty =>
		new() { SubAreaId = 0, MapId = 0, Houses = [], Actors = [], InteractiveElements = [], StatedElements = [], Obstacles = [], Fights = [] };

	public required ushort SubAreaId { get; set; }

	public required int MapId { get; set; }

	public required IEnumerable<HouseInformations> Houses { get; set; }

	public required IEnumerable<GameRolePlayActorInformations> Actors { get; set; }

	public required IEnumerable<InteractiveElement> InteractiveElements { get; set; }

	public required IEnumerable<StatedElement> StatedElements { get; set; }

	public required IEnumerable<MapObstacle> Obstacles { get; set; }

	public required IEnumerable<FightCommonInformations> Fights { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteInt32(MapId);
		var housesBefore = writer.Position;
		var housesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Houses)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			housesCount++;
		}
		var housesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, housesBefore);
		writer.WriteInt16((short)housesCount);
		writer.Seek(SeekOrigin.Begin, housesAfter);
		var actorsBefore = writer.Position;
		var actorsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Actors)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			actorsCount++;
		}
		var actorsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, actorsBefore);
		writer.WriteInt16((short)actorsCount);
		writer.Seek(SeekOrigin.Begin, actorsAfter);
		var interactiveElementsBefore = writer.Position;
		var interactiveElementsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in InteractiveElements)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			interactiveElementsCount++;
		}
		var interactiveElementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, interactiveElementsBefore);
		writer.WriteInt16((short)interactiveElementsCount);
		writer.Seek(SeekOrigin.Begin, interactiveElementsAfter);
		var statedElementsBefore = writer.Position;
		var statedElementsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in StatedElements)
		{
			item.Serialize(writer);
			statedElementsCount++;
		}
		var statedElementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, statedElementsBefore);
		writer.WriteInt16((short)statedElementsCount);
		writer.Seek(SeekOrigin.Begin, statedElementsAfter);
		var obstaclesBefore = writer.Position;
		var obstaclesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Obstacles)
		{
			item.Serialize(writer);
			obstaclesCount++;
		}
		var obstaclesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, obstaclesBefore);
		writer.WriteInt16((short)obstaclesCount);
		writer.Seek(SeekOrigin.Begin, obstaclesAfter);
		var fightsBefore = writer.Position;
		var fightsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Fights)
		{
			item.Serialize(writer);
			fightsCount++;
		}
		var fightsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, fightsBefore);
		writer.WriteInt16((short)fightsCount);
		writer.Seek(SeekOrigin.Begin, fightsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadVarUInt16();
		MapId = reader.ReadInt32();
		var housesCount = reader.ReadInt16();
		var houses = new HouseInformations[housesCount];
		for (var i = 0; i < housesCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<HouseInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			houses[i] = entry;
		}
		Houses = houses;
		var actorsCount = reader.ReadInt16();
		var actors = new GameRolePlayActorInformations[actorsCount];
		for (var i = 0; i < actorsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<GameRolePlayActorInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			actors[i] = entry;
		}
		Actors = actors;
		var interactiveElementsCount = reader.ReadInt16();
		var interactiveElements = new InteractiveElement[interactiveElementsCount];
		for (var i = 0; i < interactiveElementsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<InteractiveElement>(reader.ReadUInt16());
			entry.Deserialize(reader);
			interactiveElements[i] = entry;
		}
		InteractiveElements = interactiveElements;
		var statedElementsCount = reader.ReadInt16();
		var statedElements = new StatedElement[statedElementsCount];
		for (var i = 0; i < statedElementsCount; i++)
		{
			var entry = StatedElement.Empty;
			entry.Deserialize(reader);
			statedElements[i] = entry;
		}
		StatedElements = statedElements;
		var obstaclesCount = reader.ReadInt16();
		var obstacles = new MapObstacle[obstaclesCount];
		for (var i = 0; i < obstaclesCount; i++)
		{
			var entry = MapObstacle.Empty;
			entry.Deserialize(reader);
			obstacles[i] = entry;
		}
		Obstacles = obstacles;
		var fightsCount = reader.ReadInt16();
		var fights = new FightCommonInformations[fightsCount];
		for (var i = 0; i < fightsCount; i++)
		{
			var entry = FightCommonInformations.Empty;
			entry.Deserialize(reader);
			fights[i] = entry;
		}
		Fights = fights;
	}
}

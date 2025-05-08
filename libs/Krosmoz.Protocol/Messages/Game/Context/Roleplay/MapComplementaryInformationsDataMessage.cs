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

	public required short SubAreaId { get; set; }

	public required int MapId { get; set; }

	public required IEnumerable<HouseInformations> Houses { get; set; }

	public required IEnumerable<GameRolePlayActorInformations> Actors { get; set; }

	public required IEnumerable<InteractiveElement> InteractiveElements { get; set; }

	public required IEnumerable<StatedElement> StatedElements { get; set; }

	public required IEnumerable<MapObstacle> Obstacles { get; set; }

	public required IEnumerable<FightCommonInformations> Fights { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(SubAreaId);
		writer.WriteInt(MapId);
		var housesBefore = writer.Position;
		var housesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Houses)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			housesCount++;
		}
		var housesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, housesBefore);
		writer.WriteShort((short)housesCount);
		writer.Seek(SeekOrigin.Begin, housesAfter);
		var actorsBefore = writer.Position;
		var actorsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Actors)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			actorsCount++;
		}
		var actorsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, actorsBefore);
		writer.WriteShort((short)actorsCount);
		writer.Seek(SeekOrigin.Begin, actorsAfter);
		var interactiveElementsBefore = writer.Position;
		var interactiveElementsCount = 0;
		writer.WriteShort(0);
		foreach (var item in InteractiveElements)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			interactiveElementsCount++;
		}
		var interactiveElementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, interactiveElementsBefore);
		writer.WriteShort((short)interactiveElementsCount);
		writer.Seek(SeekOrigin.Begin, interactiveElementsAfter);
		var statedElementsBefore = writer.Position;
		var statedElementsCount = 0;
		writer.WriteShort(0);
		foreach (var item in StatedElements)
		{
			item.Serialize(writer);
			statedElementsCount++;
		}
		var statedElementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, statedElementsBefore);
		writer.WriteShort((short)statedElementsCount);
		writer.Seek(SeekOrigin.Begin, statedElementsAfter);
		var obstaclesBefore = writer.Position;
		var obstaclesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Obstacles)
		{
			item.Serialize(writer);
			obstaclesCount++;
		}
		var obstaclesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, obstaclesBefore);
		writer.WriteShort((short)obstaclesCount);
		writer.Seek(SeekOrigin.Begin, obstaclesAfter);
		var fightsBefore = writer.Position;
		var fightsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Fights)
		{
			item.Serialize(writer);
			fightsCount++;
		}
		var fightsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, fightsBefore);
		writer.WriteShort((short)fightsCount);
		writer.Seek(SeekOrigin.Begin, fightsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadShort();
		MapId = reader.ReadInt();
		var housesCount = reader.ReadShort();
		var houses = new HouseInformations[housesCount];
		for (var i = 0; i < housesCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<HouseInformations>(reader.ReadUShort());
			entry.Deserialize(reader);
			houses[i] = entry;
		}
		Houses = houses;
		var actorsCount = reader.ReadShort();
		var actors = new GameRolePlayActorInformations[actorsCount];
		for (var i = 0; i < actorsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<GameRolePlayActorInformations>(reader.ReadUShort());
			entry.Deserialize(reader);
			actors[i] = entry;
		}
		Actors = actors;
		var interactiveElementsCount = reader.ReadShort();
		var interactiveElements = new InteractiveElement[interactiveElementsCount];
		for (var i = 0; i < interactiveElementsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<InteractiveElement>(reader.ReadUShort());
			entry.Deserialize(reader);
			interactiveElements[i] = entry;
		}
		InteractiveElements = interactiveElements;
		var statedElementsCount = reader.ReadShort();
		var statedElements = new StatedElement[statedElementsCount];
		for (var i = 0; i < statedElementsCount; i++)
		{
			var entry = StatedElement.Empty;
			entry.Deserialize(reader);
			statedElements[i] = entry;
		}
		StatedElements = statedElements;
		var obstaclesCount = reader.ReadShort();
		var obstacles = new MapObstacle[obstaclesCount];
		for (var i = 0; i < obstaclesCount; i++)
		{
			var entry = MapObstacle.Empty;
			entry.Deserialize(reader);
			obstacles[i] = entry;
		}
		Obstacles = obstacles;
		var fightsCount = reader.ReadShort();
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

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Interactive;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public sealed class MapObstacleUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6051;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MapObstacleUpdateMessage Empty =>
		new() { Obstacles = [] };

	public required IEnumerable<MapObstacle> Obstacles { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
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
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var obstaclesCount = reader.ReadInt16();
		var obstacles = new MapObstacle[obstaclesCount];
		for (var i = 0; i < obstaclesCount; i++)
		{
			var entry = MapObstacle.Empty;
			entry.Deserialize(reader);
			obstacles[i] = entry;
		}
		Obstacles = obstacles;
	}
}

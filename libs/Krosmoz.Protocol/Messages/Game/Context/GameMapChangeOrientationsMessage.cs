// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameMapChangeOrientationsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6155;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameMapChangeOrientationsMessage Empty =>
		new() { Orientations = [] };

	public required IEnumerable<ActorOrientation> Orientations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var orientationsBefore = writer.Position;
		var orientationsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Orientations)
		{
			item.Serialize(writer);
			orientationsCount++;
		}
		var orientationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, orientationsBefore);
		writer.WriteInt16((short)orientationsCount);
		writer.Seek(SeekOrigin.Begin, orientationsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var orientationsCount = reader.ReadInt16();
		var orientations = new ActorOrientation[orientationsCount];
		for (var i = 0; i < orientationsCount; i++)
		{
			var entry = ActorOrientation.Empty;
			entry.Deserialize(reader);
			orientations[i] = entry;
		}
		Orientations = orientations;
	}
}

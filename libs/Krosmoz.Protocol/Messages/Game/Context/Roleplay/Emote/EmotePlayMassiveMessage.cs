// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Emote;

public sealed class EmotePlayMassiveMessage : EmotePlayAbstractMessage
{
	public new const uint StaticProtocolId = 5691;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new EmotePlayMassiveMessage Empty =>
		new() { EmoteStartTime = 0, EmoteId = 0, ActorIds = [] };

	public required IEnumerable<int> ActorIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var actorIdsBefore = writer.Position;
		var actorIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ActorIds)
		{
			writer.WriteInt32(item);
			actorIdsCount++;
		}
		var actorIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, actorIdsBefore);
		writer.WriteInt16((short)actorIdsCount);
		writer.Seek(SeekOrigin.Begin, actorIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var actorIdsCount = reader.ReadInt16();
		var actorIds = new int[actorIdsCount];
		for (var i = 0; i < actorIdsCount; i++)
		{
			actorIds[i] = reader.ReadInt32();
		}
		ActorIds = actorIds;
	}
}

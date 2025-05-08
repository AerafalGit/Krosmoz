// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Emote;

public sealed class EmoteListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5689;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static EmoteListMessage Empty =>
		new() { EmoteIds = [] };

	public required IEnumerable<sbyte> EmoteIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var emoteIdsBefore = writer.Position;
		var emoteIdsCount = 0;
		writer.WriteShort(0);
		foreach (var item in EmoteIds)
		{
			writer.WriteSByte(item);
			emoteIdsCount++;
		}
		var emoteIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, emoteIdsBefore);
		writer.WriteShort((short)emoteIdsCount);
		writer.Seek(SeekOrigin.Begin, emoteIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var emoteIdsCount = reader.ReadShort();
		var emoteIds = new sbyte[emoteIdsCount];
		for (var i = 0; i < emoteIdsCount; i++)
		{
			emoteIds[i] = reader.ReadSByte();
		}
		EmoteIds = emoteIds;
	}
}

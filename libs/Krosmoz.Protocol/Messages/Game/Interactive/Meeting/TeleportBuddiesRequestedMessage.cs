// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive.Meeting;

public sealed class TeleportBuddiesRequestedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6302;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TeleportBuddiesRequestedMessage Empty =>
		new() { DungeonId = 0, InviterId = 0, InvalidBuddiesIds = [] };

	public required short DungeonId { get; set; }

	public required int InviterId { get; set; }

	public required IEnumerable<int> InvalidBuddiesIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(DungeonId);
		writer.WriteInt(InviterId);
		var invalidBuddiesIdsBefore = writer.Position;
		var invalidBuddiesIdsCount = 0;
		writer.WriteShort(0);
		foreach (var item in InvalidBuddiesIds)
		{
			writer.WriteInt(item);
			invalidBuddiesIdsCount++;
		}
		var invalidBuddiesIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, invalidBuddiesIdsBefore);
		writer.WriteShort((short)invalidBuddiesIdsCount);
		writer.Seek(SeekOrigin.Begin, invalidBuddiesIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DungeonId = reader.ReadShort();
		InviterId = reader.ReadInt();
		var invalidBuddiesIdsCount = reader.ReadShort();
		var invalidBuddiesIds = new int[invalidBuddiesIdsCount];
		for (var i = 0; i < invalidBuddiesIdsCount; i++)
		{
			invalidBuddiesIds[i] = reader.ReadInt();
		}
		InvalidBuddiesIds = invalidBuddiesIds;
	}
}

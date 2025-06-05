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

	public required ushort DungeonId { get; set; }

	public required uint InviterId { get; set; }

	public required IEnumerable<uint> InvalidBuddiesIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(DungeonId);
		writer.WriteVarUInt32(InviterId);
		var invalidBuddiesIdsBefore = writer.Position;
		var invalidBuddiesIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in InvalidBuddiesIds)
		{
			writer.WriteVarUInt32(item);
			invalidBuddiesIdsCount++;
		}
		var invalidBuddiesIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, invalidBuddiesIdsBefore);
		writer.WriteInt16((short)invalidBuddiesIdsCount);
		writer.Seek(SeekOrigin.Begin, invalidBuddiesIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DungeonId = reader.ReadVarUInt16();
		InviterId = reader.ReadVarUInt32();
		var invalidBuddiesIdsCount = reader.ReadInt16();
		var invalidBuddiesIds = new uint[invalidBuddiesIdsCount];
		for (var i = 0; i < invalidBuddiesIdsCount; i++)
		{
			invalidBuddiesIds[i] = reader.ReadVarUInt32();
		}
		InvalidBuddiesIds = invalidBuddiesIds;
	}
}

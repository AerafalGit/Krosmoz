// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive.Meeting;

public sealed class TeleportToBuddyCloseMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6303;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TeleportToBuddyCloseMessage Empty =>
		new() { DungeonId = 0, BuddyId = 0 };

	public required ushort DungeonId { get; set; }

	public required uint BuddyId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(DungeonId);
		writer.WriteVarUInt32(BuddyId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DungeonId = reader.ReadVarUInt16();
		BuddyId = reader.ReadVarUInt32();
	}
}

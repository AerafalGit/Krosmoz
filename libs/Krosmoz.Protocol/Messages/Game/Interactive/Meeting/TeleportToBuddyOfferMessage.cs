// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive.Meeting;

public sealed class TeleportToBuddyOfferMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6287;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TeleportToBuddyOfferMessage Empty =>
		new() { DungeonId = 0, BuddyId = 0, TimeLeft = 0 };

	public required short DungeonId { get; set; }

	public required int BuddyId { get; set; }

	public required int TimeLeft { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(DungeonId);
		writer.WriteInt(BuddyId);
		writer.WriteInt(TimeLeft);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DungeonId = reader.ReadShort();
		BuddyId = reader.ReadInt();
		TimeLeft = reader.ReadInt();
	}
}

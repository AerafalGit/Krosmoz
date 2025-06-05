// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyInvitationDungeonDetailsMessage : PartyInvitationDetailsMessage
{
	public new const uint StaticProtocolId = 6262;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyInvitationDungeonDetailsMessage Empty =>
		new() { PartyId = 0, Guests = [], Members = [], LeaderId = 0, FromName = string.Empty, FromId = 0, PartyName = string.Empty, PartyType = 0, DungeonId = 0, PlayersDungeonReady = [] };

	public required ushort DungeonId { get; set; }

	public required IEnumerable<bool> PlayersDungeonReady { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(DungeonId);
		var playersDungeonReadyBefore = writer.Position;
		var playersDungeonReadyCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PlayersDungeonReady)
		{
			writer.WriteBoolean(item);
			playersDungeonReadyCount++;
		}
		var playersDungeonReadyAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, playersDungeonReadyBefore);
		writer.WriteInt16((short)playersDungeonReadyCount);
		writer.Seek(SeekOrigin.Begin, playersDungeonReadyAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		DungeonId = reader.ReadVarUInt16();
		var playersDungeonReadyCount = reader.ReadInt16();
		var playersDungeonReady = new bool[playersDungeonReadyCount];
		for (var i = 0; i < playersDungeonReadyCount; i++)
		{
			playersDungeonReady[i] = reader.ReadBoolean();
		}
		PlayersDungeonReady = playersDungeonReady;
	}
}

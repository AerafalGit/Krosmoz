// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight.Arena;

public sealed class GameRolePlayArenaFighterStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6281;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayArenaFighterStatusMessage Empty =>
		new() { FightId = 0, PlayerId = 0, Accepted = false };

	public required int FightId { get; set; }

	public required int PlayerId { get; set; }

	public required bool Accepted { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(FightId);
		writer.WriteInt(PlayerId);
		writer.WriteBoolean(Accepted);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt();
		PlayerId = reader.ReadInt();
		Accepted = reader.ReadBoolean();
	}
}

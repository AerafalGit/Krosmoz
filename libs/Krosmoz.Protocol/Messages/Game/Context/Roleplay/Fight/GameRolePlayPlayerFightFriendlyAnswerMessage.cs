// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight;

public sealed class GameRolePlayPlayerFightFriendlyAnswerMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5732;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayPlayerFightFriendlyAnswerMessage Empty =>
		new() { FightId = 0, Accept = false };

	public required int FightId { get; set; }

	public required bool Accept { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(FightId);
		writer.WriteBoolean(Accept);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt32();
		Accept = reader.ReadBoolean();
	}
}

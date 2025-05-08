// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight;

public sealed class GameRolePlayPlayerFightFriendlyAnsweredMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5733;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayPlayerFightFriendlyAnsweredMessage Empty =>
		new() { FightId = 0, SourceId = 0, TargetId = 0, Accept = false };

	public required int FightId { get; set; }

	public required int SourceId { get; set; }

	public required int TargetId { get; set; }

	public required bool Accept { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(FightId);
		writer.WriteInt(SourceId);
		writer.WriteInt(TargetId);
		writer.WriteBoolean(Accept);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt();
		SourceId = reader.ReadInt();
		TargetId = reader.ReadInt();
		Accept = reader.ReadBoolean();
	}
}

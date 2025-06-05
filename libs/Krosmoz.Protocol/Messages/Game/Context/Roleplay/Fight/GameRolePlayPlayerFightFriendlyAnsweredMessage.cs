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

	public required uint SourceId { get; set; }

	public required uint TargetId { get; set; }

	public required bool Accept { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(FightId);
		writer.WriteVarUInt32(SourceId);
		writer.WriteVarUInt32(TargetId);
		writer.WriteBoolean(Accept);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt32();
		SourceId = reader.ReadVarUInt32();
		TargetId = reader.ReadVarUInt32();
		Accept = reader.ReadBoolean();
	}
}

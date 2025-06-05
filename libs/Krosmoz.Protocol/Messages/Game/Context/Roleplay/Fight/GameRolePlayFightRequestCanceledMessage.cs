// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight;

public sealed class GameRolePlayFightRequestCanceledMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5822;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayFightRequestCanceledMessage Empty =>
		new() { FightId = 0, SourceId = 0, TargetId = 0 };

	public required int FightId { get; set; }

	public required uint SourceId { get; set; }

	public required int TargetId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(FightId);
		writer.WriteVarUInt32(SourceId);
		writer.WriteInt32(TargetId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt32();
		SourceId = reader.ReadVarUInt32();
		TargetId = reader.ReadInt32();
	}
}

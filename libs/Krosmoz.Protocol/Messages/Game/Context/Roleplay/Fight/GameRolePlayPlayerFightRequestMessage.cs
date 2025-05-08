// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight;

public sealed class GameRolePlayPlayerFightRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5731;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayPlayerFightRequestMessage Empty =>
		new() { TargetId = 0, TargetCellId = 0, Friendly = false };

	public required int TargetId { get; set; }

	public required short TargetCellId { get; set; }

	public required bool Friendly { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(TargetId);
		writer.WriteShort(TargetCellId);
		writer.WriteBoolean(Friendly);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TargetId = reader.ReadInt();
		TargetCellId = reader.ReadShort();
		Friendly = reader.ReadBoolean();
	}
}

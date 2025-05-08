// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight;

public sealed class GameRolePlayAggressionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6073;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayAggressionMessage Empty =>
		new() { AttackerId = 0, DefenderId = 0 };

	public required int AttackerId { get; set; }

	public required int DefenderId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(AttackerId);
		writer.WriteInt(DefenderId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AttackerId = reader.ReadInt();
		DefenderId = reader.ReadInt();
	}
}

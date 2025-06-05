// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight;

public sealed class GameRolePlayAttackMonsterRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6191;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayAttackMonsterRequestMessage Empty =>
		new() { MonsterGroupId = 0 };

	public required int MonsterGroupId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(MonsterGroupId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MonsterGroupId = reader.ReadInt32();
	}
}

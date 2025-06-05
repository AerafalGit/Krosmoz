// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight.Arena;

public sealed class GameRolePlayArenaRegisterMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6280;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayArenaRegisterMessage Empty =>
		new() { BattleMode = 0 };

	public required int BattleMode { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(BattleMode);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		BattleMode = reader.ReadInt32();
	}
}

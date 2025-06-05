// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight.Arena;

public sealed class GameRolePlayArenaRegistrationStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6284;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayArenaRegistrationStatusMessage Empty =>
		new() { Registered = false, Step = 0, BattleMode = 0 };

	public required bool Registered { get; set; }

	public required sbyte Step { get; set; }

	public required int BattleMode { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Registered);
		writer.WriteInt8(Step);
		writer.WriteInt32(BattleMode);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Registered = reader.ReadBoolean();
		Step = reader.ReadInt8();
		BattleMode = reader.ReadInt32();
	}
}

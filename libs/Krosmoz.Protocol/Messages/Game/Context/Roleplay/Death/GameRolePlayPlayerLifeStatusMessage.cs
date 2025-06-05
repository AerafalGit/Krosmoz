// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Death;

public sealed class GameRolePlayPlayerLifeStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5996;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayPlayerLifeStatusMessage Empty =>
		new() { State = 0 };

	public required sbyte State { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(State);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		State = reader.ReadInt8();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightTurnStartSlaveMessage : GameFightTurnStartMessage
{
	public new const uint StaticProtocolId = 6213;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameFightTurnStartSlaveMessage Empty =>
		new() { WaitTime = 0, Id = 0, IdSummoner = 0 };

	public required int IdSummoner { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(IdSummoner);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		IdSummoner = reader.ReadInt();
	}
}

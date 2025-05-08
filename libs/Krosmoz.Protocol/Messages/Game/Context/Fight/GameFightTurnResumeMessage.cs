// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightTurnResumeMessage : GameFightTurnStartMessage
{
	public new const uint StaticProtocolId = 6307;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameFightTurnResumeMessage Empty =>
		new() { WaitTime = 0, Id = 0 };
}

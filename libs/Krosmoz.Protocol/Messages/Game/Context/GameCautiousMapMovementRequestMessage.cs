// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameCautiousMapMovementRequestMessage : GameMapMovementRequestMessage
{
	public new const uint StaticProtocolId = 6496;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameCautiousMapMovementRequestMessage Empty =>
		new() { MapId = 0, KeyMovements = [] };
}

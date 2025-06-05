// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameCautiousMapMovementMessage : GameMapMovementMessage
{
	public new const uint StaticProtocolId = 6497;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameCautiousMapMovementMessage Empty =>
		new() { ActorId = 0, KeyMovements = [] };
}

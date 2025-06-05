// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight.Character;

public sealed class GameFightShowFighterRandomStaticPoseMessage : GameFightShowFighterMessage
{
	public new const uint StaticProtocolId = 6218;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameFightShowFighterRandomStaticPoseMessage Empty =>
		new() { Informations = GameFightFighterInformations.Empty };
}

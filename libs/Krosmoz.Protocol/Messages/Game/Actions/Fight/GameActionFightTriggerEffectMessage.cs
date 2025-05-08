// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightTriggerEffectMessage : GameActionFightDispellEffectMessage
{
	public new const uint StaticProtocolId = 6147;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightTriggerEffectMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, BoostUID = 0 };
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions;

public sealed class GameActionNoopMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1002;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameActionNoopMessage Empty =>
		new();
}

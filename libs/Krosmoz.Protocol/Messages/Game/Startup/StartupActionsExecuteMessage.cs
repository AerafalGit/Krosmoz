// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Startup;

public sealed class StartupActionsExecuteMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1302;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static StartupActionsExecuteMessage Empty =>
		new();
}

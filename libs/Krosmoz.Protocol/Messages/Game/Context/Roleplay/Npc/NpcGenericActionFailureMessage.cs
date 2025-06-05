// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public sealed class NpcGenericActionFailureMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5900;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NpcGenericActionFailureMessage Empty =>
		new();
}

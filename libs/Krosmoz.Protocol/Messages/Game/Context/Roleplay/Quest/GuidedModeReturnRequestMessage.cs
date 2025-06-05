// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Quest;

public sealed class GuidedModeReturnRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6088;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuidedModeReturnRequestMessage Empty =>
		new();
}

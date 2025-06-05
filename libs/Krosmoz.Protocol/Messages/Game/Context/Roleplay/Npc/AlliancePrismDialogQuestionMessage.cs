// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public sealed class AlliancePrismDialogQuestionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6448;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AlliancePrismDialogQuestionMessage Empty =>
		new();
}

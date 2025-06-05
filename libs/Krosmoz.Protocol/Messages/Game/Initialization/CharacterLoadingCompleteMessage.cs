// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Initialization;

public sealed class CharacterLoadingCompleteMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6471;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterLoadingCompleteMessage Empty =>
		new();
}

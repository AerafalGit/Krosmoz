// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharactersListErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5545;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharactersListErrorMessage Empty =>
		new();
}

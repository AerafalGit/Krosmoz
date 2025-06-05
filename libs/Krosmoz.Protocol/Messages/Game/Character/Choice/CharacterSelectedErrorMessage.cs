// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharacterSelectedErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5836;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterSelectedErrorMessage Empty =>
		new();
}

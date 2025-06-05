// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Choice;

public sealed class CharacterToRelookInformation : AbstractCharacterToRefurbishInformation
{
	public new const ushort StaticProtocolId = 399;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterToRelookInformation Empty =>
		new() { Id = 0, CosmeticId = 0, Colors = [] };
}

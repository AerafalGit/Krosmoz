// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Choice;

public sealed class CharacterToRecolorInformation : AbstractCharacterToRefurbishInformation
{
	public new const ushort StaticProtocolId = 212;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterToRecolorInformation Empty =>
		new() { Id = 0, CosmeticId = 0, Colors = [] };
}

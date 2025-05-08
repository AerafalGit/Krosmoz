// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class FightResultAdditionalData : DofusType
{
	public new const ushort StaticProtocolId = 191;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static FightResultAdditionalData Empty =>
		new();
}

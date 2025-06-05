// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntStepFight : TreasureHuntStep
{
	public new const ushort StaticProtocolId = 462;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new TreasureHuntStepFight Empty =>
		new();
}

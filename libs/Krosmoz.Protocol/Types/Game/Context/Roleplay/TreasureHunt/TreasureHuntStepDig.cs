// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntStepDig : TreasureHuntStep
{
	public new const ushort StaticProtocolId = 465;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new TreasureHuntStepDig Empty =>
		new();
}

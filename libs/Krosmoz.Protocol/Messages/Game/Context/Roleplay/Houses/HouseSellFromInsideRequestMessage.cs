// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public sealed class HouseSellFromInsideRequestMessage : HouseSellRequestMessage
{
	public new const uint StaticProtocolId = 5884;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new HouseSellFromInsideRequestMessage Empty =>
		new() { Amount = 0 };
}

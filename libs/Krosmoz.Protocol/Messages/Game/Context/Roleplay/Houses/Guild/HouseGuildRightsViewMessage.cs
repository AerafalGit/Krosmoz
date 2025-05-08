// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses.Guild;

public sealed class HouseGuildRightsViewMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5700;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseGuildRightsViewMessage Empty =>
		new();
}

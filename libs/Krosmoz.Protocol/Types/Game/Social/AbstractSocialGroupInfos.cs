// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Social;

public class AbstractSocialGroupInfos : DofusType
{
	public new const ushort StaticProtocolId = 416;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AbstractSocialGroupInfos Empty =>
		new();
}

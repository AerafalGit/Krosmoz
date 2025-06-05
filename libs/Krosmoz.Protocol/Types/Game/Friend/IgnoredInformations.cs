// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Friend;

public class IgnoredInformations : AbstractContactInformations
{
	public new const ushort StaticProtocolId = 106;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new IgnoredInformations Empty =>
		new() { AccountName = string.Empty, AccountId = 0 };
}

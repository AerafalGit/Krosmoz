// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Approach;

public sealed class ReloginTokenRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6540;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ReloginTokenRequestMessage Empty =>
		new();
}

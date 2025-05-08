// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicLatencyStatsRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5816;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicLatencyStatsRequestMessage Empty =>
		new();
}

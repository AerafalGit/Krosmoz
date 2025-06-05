// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Tinsel;

public sealed class TitlesAndOrnamentsListRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6363;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TitlesAndOrnamentsListRequestMessage Empty =>
		new();
}

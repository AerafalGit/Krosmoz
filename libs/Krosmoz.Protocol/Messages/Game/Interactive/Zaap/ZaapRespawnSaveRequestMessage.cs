// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive.Zaap;

public sealed class ZaapRespawnSaveRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6572;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ZaapRespawnSaveRequestMessage Empty =>
		new();
}

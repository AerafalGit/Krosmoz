// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Notification;

public sealed class NotificationResetMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6089;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NotificationResetMessage Empty =>
		new();
}

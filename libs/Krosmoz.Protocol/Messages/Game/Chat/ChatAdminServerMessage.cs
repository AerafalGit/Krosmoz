// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat;

public sealed class ChatAdminServerMessage : ChatServerMessage
{
	public new const uint StaticProtocolId = 6135;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ChatAdminServerMessage Empty =>
		new() { Fingerprint = string.Empty, Timestamp = 0, Content = string.Empty, Channel = 0, SenderAccountId = 0, SenderName = string.Empty, SenderId = 0 };
}

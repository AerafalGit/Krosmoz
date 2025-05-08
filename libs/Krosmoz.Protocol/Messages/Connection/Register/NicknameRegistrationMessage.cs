// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection.Register;

public sealed class NicknameRegistrationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5640;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NicknameRegistrationMessage Empty =>
		new();
}

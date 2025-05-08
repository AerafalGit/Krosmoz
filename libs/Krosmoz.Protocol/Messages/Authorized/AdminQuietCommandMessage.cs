// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Authorized;

public sealed class AdminQuietCommandMessage : AdminCommandMessage
{
	public new const uint StaticProtocolId = 5662;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new AdminQuietCommandMessage Empty =>
		new() { Content = string.Empty };
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicNoOperationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 176;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicNoOperationMessage Empty =>
		new();
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Debug;

public sealed class DebugClearHighlightCellsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 2002;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DebugClearHighlightCellsMessage Empty =>
		new();
}

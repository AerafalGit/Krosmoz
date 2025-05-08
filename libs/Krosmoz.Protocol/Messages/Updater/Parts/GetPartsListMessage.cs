// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Updater.Parts;

public sealed class GetPartsListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1501;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GetPartsListMessage Empty =>
		new();
}

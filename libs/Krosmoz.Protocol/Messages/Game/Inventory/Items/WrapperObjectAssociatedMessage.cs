// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class WrapperObjectAssociatedMessage : SymbioticObjectAssociatedMessage
{
	public new const uint StaticProtocolId = 6523;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new WrapperObjectAssociatedMessage Empty =>
		new() { HostUID = 0 };
}

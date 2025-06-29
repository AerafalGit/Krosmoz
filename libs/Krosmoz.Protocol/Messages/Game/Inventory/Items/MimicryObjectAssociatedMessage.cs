// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class MimicryObjectAssociatedMessage : SymbioticObjectAssociatedMessage
{
	public new const uint StaticProtocolId = 6462;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new MimicryObjectAssociatedMessage Empty =>
		new() { HostUID = 0 };
}

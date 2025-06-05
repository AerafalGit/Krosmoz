// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class WrapperObjectErrorMessage : SymbioticObjectErrorMessage
{
	public new const uint StaticProtocolId = 6529;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new WrapperObjectErrorMessage Empty =>
		new() { Reason = 0, ErrorCode = 0 };
}

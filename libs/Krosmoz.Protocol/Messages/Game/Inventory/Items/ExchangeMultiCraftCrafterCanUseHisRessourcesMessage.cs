// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ExchangeMultiCraftCrafterCanUseHisRessourcesMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6020;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeMultiCraftCrafterCanUseHisRessourcesMessage Empty =>
		new() { Allowed = false };

	public required bool Allowed { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Allowed);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Allowed = reader.ReadBoolean();
	}
}

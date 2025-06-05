// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6021;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage Empty =>
		new() { Allow = false };

	public required bool Allow { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Allow);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Allow = reader.ReadBoolean();
	}
}

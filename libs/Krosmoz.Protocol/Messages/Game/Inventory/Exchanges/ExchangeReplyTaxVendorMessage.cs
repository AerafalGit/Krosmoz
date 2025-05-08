// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeReplyTaxVendorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5787;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeReplyTaxVendorMessage Empty =>
		new() { ObjectValue = 0, TotalTaxValue = 0 };

	public required int ObjectValue { get; set; }

	public required int TotalTaxValue { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ObjectValue);
		writer.WriteInt(TotalTaxValue);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectValue = reader.ReadInt();
		TotalTaxValue = reader.ReadInt();
	}
}

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

	public required uint ObjectValue { get; set; }

	public required uint TotalTaxValue { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ObjectValue);
		writer.WriteVarUInt32(TotalTaxValue);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectValue = reader.ReadVarUInt32();
		TotalTaxValue = reader.ReadVarUInt32();
	}
}

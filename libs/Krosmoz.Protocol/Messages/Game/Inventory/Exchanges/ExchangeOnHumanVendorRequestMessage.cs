// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeOnHumanVendorRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5772;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeOnHumanVendorRequestMessage Empty =>
		new() { HumanVendorId = 0, HumanVendorCell = 0 };

	public required uint HumanVendorId { get; set; }

	public required ushort HumanVendorCell { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(HumanVendorId);
		writer.WriteVarUInt16(HumanVendorCell);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HumanVendorId = reader.ReadVarUInt32();
		HumanVendorCell = reader.ReadVarUInt16();
	}
}

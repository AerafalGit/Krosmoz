// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Purchasable;

public sealed class PurchasableDialogMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5739;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PurchasableDialogMessage Empty =>
		new() { BuyOrSell = false, PurchasableId = 0, Price = 0 };

	public required bool BuyOrSell { get; set; }

	public required int PurchasableId { get; set; }

	public required int Price { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(BuyOrSell);
		writer.WriteInt(PurchasableId);
		writer.WriteInt(Price);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		BuyOrSell = reader.ReadBoolean();
		PurchasableId = reader.ReadInt();
		Price = reader.ReadInt();
	}
}

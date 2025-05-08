// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeGoldPaymentForCraftMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5833;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeGoldPaymentForCraftMessage Empty =>
		new() { OnlySuccess = false, GoldSum = 0 };

	public required bool OnlySuccess { get; set; }

	public required int GoldSum { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(OnlySuccess);
		writer.WriteInt(GoldSum);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		OnlySuccess = reader.ReadBoolean();
		GoldSum = reader.ReadInt();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeItemPaymentForCraftMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5831;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeItemPaymentForCraftMessage Empty =>
		new() { OnlySuccess = false, @Object = ObjectItemNotInContainer.Empty };

	public required bool OnlySuccess { get; set; }

	public required ObjectItemNotInContainer @Object { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(OnlySuccess);
		@Object.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		OnlySuccess = reader.ReadBoolean();
		@Object = ObjectItemNotInContainer.Empty;
		@Object.Deserialize(reader);
	}
}

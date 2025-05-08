// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeRemovedPaymentForCraftMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6031;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeRemovedPaymentForCraftMessage Empty =>
		new() { OnlySuccess = false, ObjectUID = 0 };

	public required bool OnlySuccess { get; set; }

	public required int ObjectUID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(OnlySuccess);
		writer.WriteInt(ObjectUID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		OnlySuccess = reader.ReadBoolean();
		ObjectUID = reader.ReadInt();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeCraftSlotCountIncreasedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6125;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeCraftSlotCountIncreasedMessage Empty =>
		new() { NewMaxSlot = 0 };

	public required sbyte NewMaxSlot { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(NewMaxSlot);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		NewMaxSlot = reader.ReadSByte();
	}
}

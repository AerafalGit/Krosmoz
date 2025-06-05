// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartOkNpcTradeMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5785;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeStartOkNpcTradeMessage Empty =>
		new() { NpcId = 0 };

	public required int NpcId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(NpcId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		NpcId = reader.ReadInt32();
	}
}

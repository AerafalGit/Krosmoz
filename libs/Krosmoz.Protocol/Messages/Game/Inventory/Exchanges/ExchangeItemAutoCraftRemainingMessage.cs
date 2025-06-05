// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeItemAutoCraftRemainingMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6015;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeItemAutoCraftRemainingMessage Empty =>
		new() { Count = 0 };

	public required uint Count { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Count);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Count = reader.ReadVarUInt32();
	}
}

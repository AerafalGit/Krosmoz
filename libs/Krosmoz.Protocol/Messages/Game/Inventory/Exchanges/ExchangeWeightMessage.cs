// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeWeightMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5793;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeWeightMessage Empty =>
		new() { CurrentWeight = 0, MaxWeight = 0 };

	public required uint CurrentWeight { get; set; }

	public required uint MaxWeight { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(CurrentWeight);
		writer.WriteVarUInt32(MaxWeight);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CurrentWeight = reader.ReadVarUInt32();
		MaxWeight = reader.ReadVarUInt32();
	}
}

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

	public required int CurrentWeight { get; set; }

	public required int MaxWeight { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(CurrentWeight);
		writer.WriteInt32(MaxWeight);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CurrentWeight = reader.ReadInt32();
		MaxWeight = reader.ReadInt32();
	}
}

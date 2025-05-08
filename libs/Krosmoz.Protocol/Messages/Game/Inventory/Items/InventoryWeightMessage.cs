// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class InventoryWeightMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3009;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryWeightMessage Empty =>
		new() { Weight = 0, WeightMax = 0 };

	public required int Weight { get; set; }

	public required int WeightMax { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(Weight);
		writer.WriteInt(WeightMax);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Weight = reader.ReadInt();
		WeightMax = reader.ReadInt();
	}
}

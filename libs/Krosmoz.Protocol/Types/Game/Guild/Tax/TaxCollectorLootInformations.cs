// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Guild.Tax;

public sealed class TaxCollectorLootInformations : TaxCollectorComplementaryInformations
{
	public new const ushort StaticProtocolId = 372;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new TaxCollectorLootInformations Empty =>
		new() { Kamas = 0, Experience = 0, Pods = 0, ItemsValue = 0 };

	public required uint Kamas { get; set; }

	public required ulong Experience { get; set; }

	public required uint Pods { get; set; }

	public required uint ItemsValue { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Kamas);
		writer.WriteVarUInt64(Experience);
		writer.WriteVarUInt32(Pods);
		writer.WriteVarUInt32(ItemsValue);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Kamas = reader.ReadVarUInt32();
		Experience = reader.ReadVarUInt64();
		Pods = reader.ReadVarUInt32();
		ItemsValue = reader.ReadVarUInt32();
	}
}

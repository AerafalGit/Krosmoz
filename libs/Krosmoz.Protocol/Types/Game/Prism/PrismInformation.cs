// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Prism;

public class PrismInformation : DofusType
{
	public new const ushort StaticProtocolId = 428;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static PrismInformation Empty =>
		new() { TypeId = 0, State = 0, NextVulnerabilityDate = 0, PlacementDate = 0, RewardTokenCount = 0 };

	public required sbyte TypeId { get; set; }

	public required sbyte State { get; set; }

	public required int NextVulnerabilityDate { get; set; }

	public required int PlacementDate { get; set; }

	public required uint RewardTokenCount { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(TypeId);
		writer.WriteInt8(State);
		writer.WriteInt32(NextVulnerabilityDate);
		writer.WriteInt32(PlacementDate);
		writer.WriteVarUInt32(RewardTokenCount);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TypeId = reader.ReadInt8();
		State = reader.ReadInt8();
		NextVulnerabilityDate = reader.ReadInt32();
		PlacementDate = reader.ReadInt32();
		RewardTokenCount = reader.ReadVarUInt32();
	}
}

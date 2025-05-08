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
		new() { TypeId = 0, State = 0, NextVulnerabilityDate = 0, PlacementDate = 0 };

	public required sbyte TypeId { get; set; }

	public required sbyte State { get; set; }

	public required int NextVulnerabilityDate { get; set; }

	public required int PlacementDate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(TypeId);
		writer.WriteSByte(State);
		writer.WriteInt(NextVulnerabilityDate);
		writer.WriteInt(PlacementDate);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TypeId = reader.ReadSByte();
		State = reader.ReadSByte();
		NextVulnerabilityDate = reader.ReadInt();
		PlacementDate = reader.ReadInt();
	}
}

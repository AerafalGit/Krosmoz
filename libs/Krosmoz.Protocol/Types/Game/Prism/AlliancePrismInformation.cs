// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Types.Game.Prism;

public sealed class AlliancePrismInformation : PrismInformation
{
	public new const ushort StaticProtocolId = 427;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new AlliancePrismInformation Empty =>
		new() { RewardTokenCount = 0, PlacementDate = 0, NextVulnerabilityDate = 0, State = 0, TypeId = 0, Alliance = AllianceInformations.Empty };

	public required AllianceInformations Alliance { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Alliance.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Alliance = AllianceInformations.Empty;
		Alliance.Deserialize(reader);
	}
}

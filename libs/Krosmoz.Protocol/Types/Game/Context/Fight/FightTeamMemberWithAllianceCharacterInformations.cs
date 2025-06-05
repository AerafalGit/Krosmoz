// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightTeamMemberWithAllianceCharacterInformations : FightTeamMemberCharacterInformations
{
	public new const ushort StaticProtocolId = 426;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightTeamMemberWithAllianceCharacterInformations Empty =>
		new() { Id = 0, Level = 0, Name = string.Empty, AllianceInfos = BasicAllianceInformations.Empty };

	public required BasicAllianceInformations AllianceInfos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		AllianceInfos.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AllianceInfos = BasicAllianceInformations.Empty;
		AllianceInfos.Deserialize(reader);
	}
}

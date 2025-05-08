// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Types.Game.Social;

public sealed class AlliancedGuildFactSheetInformations : GuildInformations
{
	public new const ushort StaticProtocolId = 422;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new AlliancedGuildFactSheetInformations Empty =>
		new() { GuildName = string.Empty, GuildId = 0, GuildEmblem = GuildEmblem.Empty, AllianceInfos = BasicNamedAllianceInformations.Empty };

	public required BasicNamedAllianceInformations AllianceInfos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		AllianceInfos.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AllianceInfos = BasicNamedAllianceInformations.Empty;
		AllianceInfos.Deserialize(reader);
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public class AllianceInformations : BasicNamedAllianceInformations
{
	public new const ushort StaticProtocolId = 417;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new AllianceInformations Empty =>
		new() { AllianceTag = string.Empty, AllianceId = 0, AllianceName = string.Empty, AllianceEmblem = GuildEmblem.Empty };

	public required GuildEmblem AllianceEmblem { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		AllianceEmblem.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AllianceEmblem = GuildEmblem.Empty;
		AllianceEmblem.Deserialize(reader);
	}
}

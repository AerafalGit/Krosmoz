// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Social;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildInAllianceFactsMessage : GuildFactsMessage
{
	public new const uint StaticProtocolId = 6422;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GuildInAllianceFactsMessage Empty =>
		new() { Members = [], Enabled = false, NbTaxCollectors = 0, CreationDate = 0, Infos = GuildFactSheetInformations.Empty, AllianceInfos = BasicNamedAllianceInformations.Empty };

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

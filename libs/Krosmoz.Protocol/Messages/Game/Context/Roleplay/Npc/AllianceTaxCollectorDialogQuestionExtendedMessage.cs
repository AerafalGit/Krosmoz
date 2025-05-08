// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public sealed class AllianceTaxCollectorDialogQuestionExtendedMessage : TaxCollectorDialogQuestionExtendedMessage
{
	public new const uint StaticProtocolId = 6445;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new AllianceTaxCollectorDialogQuestionExtendedMessage Empty =>
		new() { GuildInfo = BasicGuildInformations.Empty, ItemsValue = 0, Pods = 0, Experience = 0, Kamas = 0, TaxCollectorAttack = 0, TaxCollectorsCount = 0, Wisdom = 0, Prospecting = 0, MaxPods = 0, Alliance = BasicNamedAllianceInformations.Empty };

	public required BasicNamedAllianceInformations Alliance { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Alliance.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Alliance = BasicNamedAllianceInformations.Empty;
		Alliance.Deserialize(reader);
	}
}

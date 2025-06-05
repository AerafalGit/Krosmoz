// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public class TaxCollectorDialogQuestionBasicMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5619;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TaxCollectorDialogQuestionBasicMessage Empty =>
		new() { GuildInfo = BasicGuildInformations.Empty };

	public required BasicGuildInformations GuildInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		GuildInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildInfo = BasicGuildInformations.Empty;
		GuildInfo.Deserialize(reader);
	}
}

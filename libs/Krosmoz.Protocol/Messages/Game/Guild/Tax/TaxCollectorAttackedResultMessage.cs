// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Guild.Tax;

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class TaxCollectorAttackedResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5635;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TaxCollectorAttackedResultMessage Empty =>
		new() { DeadOrAlive = false, BasicInfos = TaxCollectorBasicInformations.Empty, Guild = BasicGuildInformations.Empty };

	public required bool DeadOrAlive { get; set; }

	public required TaxCollectorBasicInformations BasicInfos { get; set; }

	public required BasicGuildInformations Guild { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(DeadOrAlive);
		BasicInfos.Serialize(writer);
		Guild.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DeadOrAlive = reader.ReadBoolean();
		BasicInfos = TaxCollectorBasicInformations.Empty;
		BasicInfos.Deserialize(reader);
		Guild = BasicGuildInformations.Empty;
		Guild.Deserialize(reader);
	}
}

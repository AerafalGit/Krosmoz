// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Stats;

public sealed class StatsUpgradeResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5609;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static StatsUpgradeResultMessage Empty =>
		new() { NbCharacBoost = 0 };

	public required short NbCharacBoost { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(NbCharacBoost);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		NbCharacBoost = reader.ReadShort();
	}
}

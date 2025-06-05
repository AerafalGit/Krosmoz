// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntAvailableRetryCountUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6491;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TreasureHuntAvailableRetryCountUpdateMessage Empty =>
		new() { QuestType = 0, AvailableRetryCount = 0 };

	public required sbyte QuestType { get; set; }

	public required int AvailableRetryCount { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(QuestType);
		writer.WriteInt32(AvailableRetryCount);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		QuestType = reader.ReadInt8();
		AvailableRetryCount = reader.ReadInt32();
	}
}

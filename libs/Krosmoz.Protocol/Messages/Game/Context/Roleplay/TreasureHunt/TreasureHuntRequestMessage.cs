// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6488;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TreasureHuntRequestMessage Empty =>
		new() { QuestLevel = 0, QuestType = 0 };

	public required byte QuestLevel { get; set; }

	public required sbyte QuestType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt8(QuestLevel);
		writer.WriteInt8(QuestType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		QuestLevel = reader.ReadUInt8();
		QuestType = reader.ReadInt8();
	}
}

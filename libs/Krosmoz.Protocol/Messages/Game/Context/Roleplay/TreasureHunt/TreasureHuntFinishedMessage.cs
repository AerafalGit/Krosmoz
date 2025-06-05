// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntFinishedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6483;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TreasureHuntFinishedMessage Empty =>
		new() { QuestType = 0 };

	public required sbyte QuestType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(QuestType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		QuestType = reader.ReadInt8();
	}
}

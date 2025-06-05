// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntFlagRemoveRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6510;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TreasureHuntFlagRemoveRequestMessage Empty =>
		new() { QuestType = 0, Index = 0 };

	public required sbyte QuestType { get; set; }

	public required sbyte Index { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(QuestType);
		writer.WriteInt8(Index);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		QuestType = reader.ReadInt8();
		Index = reader.ReadInt8();
	}
}

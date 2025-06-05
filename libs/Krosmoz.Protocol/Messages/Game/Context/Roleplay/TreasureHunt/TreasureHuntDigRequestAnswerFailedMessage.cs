// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntDigRequestAnswerFailedMessage : TreasureHuntDigRequestAnswerMessage
{
	public new const uint StaticProtocolId = 6509;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new TreasureHuntDigRequestAnswerFailedMessage Empty =>
		new() { Result = 0, QuestType = 0, WrongFlagCount = 0 };

	public required sbyte WrongFlagCount { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(WrongFlagCount);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		WrongFlagCount = reader.ReadInt8();
	}
}

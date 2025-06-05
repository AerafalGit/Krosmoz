// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat.Smiley;

public sealed class MoodSmileyUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6388;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MoodSmileyUpdateMessage Empty =>
		new() { AccountId = 0, PlayerId = 0, SmileyId = 0 };

	public required int AccountId { get; set; }

	public required uint PlayerId { get; set; }

	public required sbyte SmileyId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(AccountId);
		writer.WriteVarUInt32(PlayerId);
		writer.WriteInt8(SmileyId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AccountId = reader.ReadInt32();
		PlayerId = reader.ReadVarUInt32();
		SmileyId = reader.ReadInt8();
	}
}

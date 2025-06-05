// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat.Smiley;

public sealed class MoodSmileyResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6196;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MoodSmileyResultMessage Empty =>
		new() { ResultCode = 0, SmileyId = 0 };

	public required sbyte ResultCode { get; set; }

	public required sbyte SmileyId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(ResultCode);
		writer.WriteInt8(SmileyId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ResultCode = reader.ReadInt8();
		SmileyId = reader.ReadInt8();
	}
}

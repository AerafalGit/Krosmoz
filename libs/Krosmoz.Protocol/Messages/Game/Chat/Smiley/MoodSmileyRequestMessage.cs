// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Chat.Smiley;

public sealed class MoodSmileyRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6192;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MoodSmileyRequestMessage Empty =>
		new() { SmileyId = 0 };

	public required sbyte SmileyId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(SmileyId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SmileyId = reader.ReadInt8();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Moderation;

public sealed class PopupWarningMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6134;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PopupWarningMessage Empty =>
		new() { LockDuration = 0, Author = string.Empty, Content = string.Empty };

	public required byte LockDuration { get; set; }

	public required string Author { get; set; }

	public required string Content { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt8(LockDuration);
		writer.WriteUtfPrefixedLength16(Author);
		writer.WriteUtfPrefixedLength16(Content);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		LockDuration = reader.ReadUInt8();
		Author = reader.ReadUtfPrefixedLength16();
		Content = reader.ReadUtfPrefixedLength16();
	}
}

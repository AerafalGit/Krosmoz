// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Sequence;

public sealed class SequenceStartMessage : DofusMessage
{
	public new const uint StaticProtocolId = 955;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SequenceStartMessage Empty =>
		new() { SequenceType = 0, AuthorId = 0 };

	public required sbyte SequenceType { get; set; }

	public required int AuthorId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(SequenceType);
		writer.WriteInt32(AuthorId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SequenceType = reader.ReadInt8();
		AuthorId = reader.ReadInt32();
	}
}

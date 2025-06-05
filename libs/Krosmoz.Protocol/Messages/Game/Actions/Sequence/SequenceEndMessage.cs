// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Sequence;

public sealed class SequenceEndMessage : DofusMessage
{
	public new const uint StaticProtocolId = 956;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SequenceEndMessage Empty =>
		new() { ActionId = 0, AuthorId = 0, SequenceType = 0 };

	public required ushort ActionId { get; set; }

	public required int AuthorId { get; set; }

	public required sbyte SequenceType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ActionId);
		writer.WriteInt32(AuthorId);
		writer.WriteInt8(SequenceType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ActionId = reader.ReadVarUInt16();
		AuthorId = reader.ReadInt32();
		SequenceType = reader.ReadInt8();
	}
}

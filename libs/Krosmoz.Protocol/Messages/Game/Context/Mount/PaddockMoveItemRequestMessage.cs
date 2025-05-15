// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class PaddockMoveItemRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6052;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PaddockMoveItemRequestMessage Empty =>
		new() { OldCellId = 0, NewCellId = 0 };

	public required short OldCellId { get; set; }

	public required short NewCellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(OldCellId);
		writer.WriteInt16(NewCellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		OldCellId = reader.ReadInt16();
		NewCellId = reader.ReadInt16();
	}
}

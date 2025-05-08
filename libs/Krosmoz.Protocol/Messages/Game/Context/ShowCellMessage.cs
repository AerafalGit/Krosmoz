// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public class ShowCellMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5612;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ShowCellMessage Empty =>
		new() { SourceId = 0, CellId = 0 };

	public required int SourceId { get; set; }

	public required short CellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(SourceId);
		writer.WriteShort(CellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SourceId = reader.ReadInt();
		CellId = reader.ReadShort();
	}
}

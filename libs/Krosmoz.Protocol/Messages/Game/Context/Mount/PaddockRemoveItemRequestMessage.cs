// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class PaddockRemoveItemRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5958;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PaddockRemoveItemRequestMessage Empty =>
		new() { CellId = 0 };

	public required short CellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(CellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CellId = reader.ReadInt16();
	}
}

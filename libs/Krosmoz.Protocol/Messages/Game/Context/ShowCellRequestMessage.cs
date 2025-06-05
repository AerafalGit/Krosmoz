// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class ShowCellRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5611;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ShowCellRequestMessage Empty =>
		new() { CellId = 0 };

	public required ushort CellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(CellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CellId = reader.ReadVarUInt16();
	}
}

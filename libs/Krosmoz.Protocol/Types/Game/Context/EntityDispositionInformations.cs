// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context;

public class EntityDispositionInformations : DofusType
{
	public new const ushort StaticProtocolId = 60;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static EntityDispositionInformations Empty =>
		new() { CellId = 0, Direction = 0 };

	public required short CellId { get; set; }

	public required sbyte Direction { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(CellId);
		writer.WriteInt8(Direction);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CellId = reader.ReadInt16();
		Direction = reader.ReadInt8();
	}
}

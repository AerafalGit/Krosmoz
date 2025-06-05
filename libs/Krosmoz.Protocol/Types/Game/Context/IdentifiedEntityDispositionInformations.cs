// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context;

public sealed class IdentifiedEntityDispositionInformations : EntityDispositionInformations
{
	public new const ushort StaticProtocolId = 107;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new IdentifiedEntityDispositionInformations Empty =>
		new() { Direction = 0, CellId = 0, Id = 0 };

	public required int Id { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(Id);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Id = reader.ReadInt32();
	}
}

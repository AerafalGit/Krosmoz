// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context;

public sealed class FightEntityDispositionInformations : EntityDispositionInformations
{
	public new const ushort StaticProtocolId = 217;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightEntityDispositionInformations Empty =>
		new() { Direction = 0, CellId = 0, CarryingCharacterId = 0 };

	public required int CarryingCharacterId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(CarryingCharacterId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CarryingCharacterId = reader.ReadInt32();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightCastRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1005;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameActionFightCastRequestMessage Empty =>
		new() { SpellId = 0, CellId = 0 };

	public required ushort SpellId { get; set; }

	public required short CellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SpellId);
		writer.WriteInt16(CellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpellId = reader.ReadVarUInt16();
		CellId = reader.ReadInt16();
	}
}

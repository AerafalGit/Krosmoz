// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightCastOnTargetRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6330;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameActionFightCastOnTargetRequestMessage Empty =>
		new() { SpellId = 0, TargetId = 0 };

	public required ushort SpellId { get; set; }

	public required int TargetId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SpellId);
		writer.WriteInt32(TargetId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpellId = reader.ReadVarUInt16();
		TargetId = reader.ReadInt32();
	}
}

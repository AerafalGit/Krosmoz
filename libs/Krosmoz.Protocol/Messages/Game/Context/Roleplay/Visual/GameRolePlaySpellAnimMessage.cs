// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Visual;

public sealed class GameRolePlaySpellAnimMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6114;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlaySpellAnimMessage Empty =>
		new() { CasterId = 0, TargetCellId = 0, SpellId = 0, SpellLevel = 0 };

	public required int CasterId { get; set; }

	public required ushort TargetCellId { get; set; }

	public required ushort SpellId { get; set; }

	public required sbyte SpellLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(CasterId);
		writer.WriteVarUInt16(TargetCellId);
		writer.WriteVarUInt16(SpellId);
		writer.WriteInt8(SpellLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CasterId = reader.ReadInt32();
		TargetCellId = reader.ReadVarUInt16();
		SpellId = reader.ReadVarUInt16();
		SpellLevel = reader.ReadInt8();
	}
}

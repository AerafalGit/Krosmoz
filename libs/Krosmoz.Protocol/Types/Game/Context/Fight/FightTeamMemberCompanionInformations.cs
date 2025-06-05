// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightTeamMemberCompanionInformations : FightTeamMemberInformations
{
	public new const ushort StaticProtocolId = 451;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightTeamMemberCompanionInformations Empty =>
		new() { Id = 0, CompanionId = 0, Level = 0, MasterId = 0 };

	public required sbyte CompanionId { get; set; }

	public required byte Level { get; set; }

	public required int MasterId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(CompanionId);
		writer.WriteUInt8(Level);
		writer.WriteInt32(MasterId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CompanionId = reader.ReadInt8();
		Level = reader.ReadUInt8();
		MasterId = reader.ReadInt32();
	}
}

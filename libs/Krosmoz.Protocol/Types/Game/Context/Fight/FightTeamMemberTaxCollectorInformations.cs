// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightTeamMemberTaxCollectorInformations : FightTeamMemberInformations
{
	public new const ushort StaticProtocolId = 177;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightTeamMemberTaxCollectorInformations Empty =>
		new() { Id = 0, FirstNameId = 0, LastNameId = 0, Level = 0, GuildId = 0, Uid = 0 };

	public required ushort FirstNameId { get; set; }

	public required ushort LastNameId { get; set; }

	public required byte Level { get; set; }

	public required uint GuildId { get; set; }

	public required uint Uid { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(FirstNameId);
		writer.WriteVarUInt16(LastNameId);
		writer.WriteUInt8(Level);
		writer.WriteVarUInt32(GuildId);
		writer.WriteVarUInt32(Uid);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		FirstNameId = reader.ReadVarUInt16();
		LastNameId = reader.ReadVarUInt16();
		Level = reader.ReadUInt8();
		GuildId = reader.ReadVarUInt32();
		Uid = reader.ReadVarUInt32();
	}
}

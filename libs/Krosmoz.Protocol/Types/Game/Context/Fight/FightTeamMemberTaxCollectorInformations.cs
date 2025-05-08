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

	public required short FirstNameId { get; set; }

	public required short LastNameId { get; set; }

	public required byte Level { get; set; }

	public required int GuildId { get; set; }

	public required int Uid { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(FirstNameId);
		writer.WriteShort(LastNameId);
		writer.WriteByte(Level);
		writer.WriteInt(GuildId);
		writer.WriteInt(Uid);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		FirstNameId = reader.ReadShort();
		LastNameId = reader.ReadShort();
		Level = reader.ReadByte();
		GuildId = reader.ReadInt();
		Uid = reader.ReadInt();
	}
}

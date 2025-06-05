// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightTeamMemberMonsterInformations : FightTeamMemberInformations
{
	public new const ushort StaticProtocolId = 6;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightTeamMemberMonsterInformations Empty =>
		new() { Id = 0, MonsterId = 0, Grade = 0 };

	public required int MonsterId { get; set; }

	public required sbyte Grade { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(MonsterId);
		writer.WriteInt8(Grade);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MonsterId = reader.ReadInt32();
		Grade = reader.ReadInt8();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightCommonInformations : DofusType
{
	public new const ushort StaticProtocolId = 43;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static FightCommonInformations Empty =>
		new() { FightId = 0, FightType = 0, FightTeams = [], FightTeamsPositions = [], FightTeamsOptions = [] };

	public required int FightId { get; set; }

	public required sbyte FightType { get; set; }

	public required IEnumerable<FightTeamInformations> FightTeams { get; set; }

	public required IEnumerable<short> FightTeamsPositions { get; set; }

	public required IEnumerable<FightOptionsInformations> FightTeamsOptions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(FightId);
		writer.WriteSByte(FightType);
		var fightTeamsBefore = writer.Position;
		var fightTeamsCount = 0;
		writer.WriteShort(0);
		foreach (var item in FightTeams)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			fightTeamsCount++;
		}
		var fightTeamsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, fightTeamsBefore);
		writer.WriteShort((short)fightTeamsCount);
		writer.Seek(SeekOrigin.Begin, fightTeamsAfter);
		var fightTeamsPositionsBefore = writer.Position;
		var fightTeamsPositionsCount = 0;
		writer.WriteShort(0);
		foreach (var item in FightTeamsPositions)
		{
			writer.WriteShort(item);
			fightTeamsPositionsCount++;
		}
		var fightTeamsPositionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, fightTeamsPositionsBefore);
		writer.WriteShort((short)fightTeamsPositionsCount);
		writer.Seek(SeekOrigin.Begin, fightTeamsPositionsAfter);
		var fightTeamsOptionsBefore = writer.Position;
		var fightTeamsOptionsCount = 0;
		writer.WriteShort(0);
		foreach (var item in FightTeamsOptions)
		{
			item.Serialize(writer);
			fightTeamsOptionsCount++;
		}
		var fightTeamsOptionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, fightTeamsOptionsBefore);
		writer.WriteShort((short)fightTeamsOptionsCount);
		writer.Seek(SeekOrigin.Begin, fightTeamsOptionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt();
		FightType = reader.ReadSByte();
		var fightTeamsCount = reader.ReadShort();
		var fightTeams = new FightTeamInformations[fightTeamsCount];
		for (var i = 0; i < fightTeamsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<FightTeamInformations>(reader.ReadUShort());
			entry.Deserialize(reader);
			fightTeams[i] = entry;
		}
		FightTeams = fightTeams;
		var fightTeamsPositionsCount = reader.ReadShort();
		var fightTeamsPositions = new short[fightTeamsPositionsCount];
		for (var i = 0; i < fightTeamsPositionsCount; i++)
		{
			fightTeamsPositions[i] = reader.ReadShort();
		}
		FightTeamsPositions = fightTeamsPositions;
		var fightTeamsOptionsCount = reader.ReadShort();
		var fightTeamsOptions = new FightOptionsInformations[fightTeamsOptionsCount];
		for (var i = 0; i < fightTeamsOptionsCount; i++)
		{
			var entry = FightOptionsInformations.Empty;
			entry.Deserialize(reader);
			fightTeamsOptions[i] = entry;
		}
		FightTeamsOptions = fightTeamsOptions;
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightExternalInformations : DofusType
{
	public new const ushort StaticProtocolId = 117;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static FightExternalInformations Empty =>
		new() { FightId = 0, FightType = 0, FightStart = 0, FightSpectatorLocked = false, FightTeams = [], FightTeamsOptions = [] };

	public required int FightId { get; set; }

	public required sbyte FightType { get; set; }

	public required int FightStart { get; set; }

	public required bool FightSpectatorLocked { get; set; }

	public required IEnumerable<FightTeamLightInformations> FightTeams { get; set; }

	public required IEnumerable<FightOptionsInformations> FightTeamsOptions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(FightId);
		writer.WriteSByte(FightType);
		writer.WriteInt(FightStart);
		writer.WriteBoolean(FightSpectatorLocked);
		var fightTeamsBefore = writer.Position;
		var fightTeamsCount = 0;
		writer.WriteShort(0);
		foreach (var item in FightTeams)
		{
			item.Serialize(writer);
			fightTeamsCount++;
		}
		var fightTeamsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, fightTeamsBefore);
		writer.WriteShort((short)fightTeamsCount);
		writer.Seek(SeekOrigin.Begin, fightTeamsAfter);
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
		FightStart = reader.ReadInt();
		FightSpectatorLocked = reader.ReadBoolean();
		var fightTeamsCount = reader.ReadShort();
		var fightTeams = new FightTeamLightInformations[fightTeamsCount];
		for (var i = 0; i < fightTeamsCount; i++)
		{
			var entry = FightTeamLightInformations.Empty;
			entry.Deserialize(reader);
			fightTeams[i] = entry;
		}
		FightTeams = fightTeams;
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

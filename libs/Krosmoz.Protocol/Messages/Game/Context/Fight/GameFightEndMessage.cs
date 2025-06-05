// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;
using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightEndMessage : DofusMessage
{
	public new const uint StaticProtocolId = 720;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightEndMessage Empty =>
		new() { Duration = 0, AgeBonus = 0, LootShareLimitMalus = 0, Results = [], NamedPartyTeamsOutcomes = [] };

	public required int Duration { get; set; }

	public required short AgeBonus { get; set; }

	public required short LootShareLimitMalus { get; set; }

	public required IEnumerable<FightResultListEntry> Results { get; set; }

	public required IEnumerable<NamedPartyTeamWithOutcome> NamedPartyTeamsOutcomes { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(Duration);
		writer.WriteInt16(AgeBonus);
		writer.WriteInt16(LootShareLimitMalus);
		var resultsBefore = writer.Position;
		var resultsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Results)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			resultsCount++;
		}
		var resultsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, resultsBefore);
		writer.WriteInt16((short)resultsCount);
		writer.Seek(SeekOrigin.Begin, resultsAfter);
		var namedPartyTeamsOutcomesBefore = writer.Position;
		var namedPartyTeamsOutcomesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in NamedPartyTeamsOutcomes)
		{
			item.Serialize(writer);
			namedPartyTeamsOutcomesCount++;
		}
		var namedPartyTeamsOutcomesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, namedPartyTeamsOutcomesBefore);
		writer.WriteInt16((short)namedPartyTeamsOutcomesCount);
		writer.Seek(SeekOrigin.Begin, namedPartyTeamsOutcomesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Duration = reader.ReadInt32();
		AgeBonus = reader.ReadInt16();
		LootShareLimitMalus = reader.ReadInt16();
		var resultsCount = reader.ReadInt16();
		var results = new FightResultListEntry[resultsCount];
		for (var i = 0; i < resultsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<FightResultListEntry>(reader.ReadUInt16());
			entry.Deserialize(reader);
			results[i] = entry;
		}
		Results = results;
		var namedPartyTeamsOutcomesCount = reader.ReadInt16();
		var namedPartyTeamsOutcomes = new NamedPartyTeamWithOutcome[namedPartyTeamsOutcomesCount];
		for (var i = 0; i < namedPartyTeamsOutcomesCount; i++)
		{
			var entry = NamedPartyTeamWithOutcome.Empty;
			entry.Deserialize(reader);
			namedPartyTeamsOutcomes[i] = entry;
		}
		NamedPartyTeamsOutcomes = namedPartyTeamsOutcomes;
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightEndMessage : DofusMessage
{
	public new const uint StaticProtocolId = 720;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightEndMessage Empty =>
		new() { Duration = 0, AgeBonus = 0, LootShareLimitMalus = 0, Results = [] };

	public required int Duration { get; set; }

	public required short AgeBonus { get; set; }

	public required short LootShareLimitMalus { get; set; }

	public required IEnumerable<FightResultListEntry> Results { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(Duration);
		writer.WriteShort(AgeBonus);
		writer.WriteShort(LootShareLimitMalus);
		var resultsBefore = writer.Position;
		var resultsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Results)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			resultsCount++;
		}
		var resultsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, resultsBefore);
		writer.WriteShort((short)resultsCount);
		writer.Seek(SeekOrigin.Begin, resultsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Duration = reader.ReadInt();
		AgeBonus = reader.ReadShort();
		LootShareLimitMalus = reader.ReadShort();
		var resultsCount = reader.ReadShort();
		var results = new FightResultListEntry[resultsCount];
		for (var i = 0; i < resultsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<FightResultListEntry>(reader.ReadUShort());
			entry.Deserialize(reader);
			results[i] = entry;
		}
		Results = results;
	}
}

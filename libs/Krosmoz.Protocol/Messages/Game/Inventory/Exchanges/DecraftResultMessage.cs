// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class DecraftResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6569;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DecraftResultMessage Empty =>
		new() { Results = [] };

	public required IEnumerable<DecraftedItemStackInfo> Results { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var resultsBefore = writer.Position;
		var resultsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Results)
		{
			item.Serialize(writer);
			resultsCount++;
		}
		var resultsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, resultsBefore);
		writer.WriteInt16((short)resultsCount);
		writer.Seek(SeekOrigin.Begin, resultsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var resultsCount = reader.ReadInt16();
		var results = new DecraftedItemStackInfo[resultsCount];
		for (var i = 0; i < resultsCount; i++)
		{
			var entry = DecraftedItemStackInfo.Empty;
			entry.Deserialize(reader);
			results[i] = entry;
		}
		Results = results;
	}
}

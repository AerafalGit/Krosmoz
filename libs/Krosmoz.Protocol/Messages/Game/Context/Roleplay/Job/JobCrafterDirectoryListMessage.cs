// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectoryListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6046;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectoryListMessage Empty =>
		new() { ListEntries = [] };

	public required IEnumerable<JobCrafterDirectoryListEntry> ListEntries { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var listEntriesBefore = writer.Position;
		var listEntriesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ListEntries)
		{
			item.Serialize(writer);
			listEntriesCount++;
		}
		var listEntriesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, listEntriesBefore);
		writer.WriteInt16((short)listEntriesCount);
		writer.Seek(SeekOrigin.Begin, listEntriesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var listEntriesCount = reader.ReadInt16();
		var listEntries = new JobCrafterDirectoryListEntry[listEntriesCount];
		for (var i = 0; i < listEntriesCount; i++)
		{
			var entry = JobCrafterDirectoryListEntry.Empty;
			entry.Deserialize(reader);
			listEntries[i] = entry;
		}
		ListEntries = listEntries;
	}
}

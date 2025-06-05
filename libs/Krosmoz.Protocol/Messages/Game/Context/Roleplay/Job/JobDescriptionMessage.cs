// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobDescriptionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5655;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobDescriptionMessage Empty =>
		new() { JobsDescription = [] };

	public required IEnumerable<JobDescription> JobsDescription { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var jobsDescriptionBefore = writer.Position;
		var jobsDescriptionCount = 0;
		writer.WriteInt16(0);
		foreach (var item in JobsDescription)
		{
			item.Serialize(writer);
			jobsDescriptionCount++;
		}
		var jobsDescriptionAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, jobsDescriptionBefore);
		writer.WriteInt16((short)jobsDescriptionCount);
		writer.Seek(SeekOrigin.Begin, jobsDescriptionAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var jobsDescriptionCount = reader.ReadInt16();
		var jobsDescription = new JobDescription[jobsDescriptionCount];
		for (var i = 0; i < jobsDescriptionCount; i++)
		{
			var entry = JobDescription.Empty;
			entry.Deserialize(reader);
			jobsDescription[i] = entry;
		}
		JobsDescription = jobsDescription;
	}
}

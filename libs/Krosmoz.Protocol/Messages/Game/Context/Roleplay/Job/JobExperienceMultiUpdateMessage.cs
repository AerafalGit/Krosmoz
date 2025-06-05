// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobExperienceMultiUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5809;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobExperienceMultiUpdateMessage Empty =>
		new() { ExperiencesUpdate = [] };

	public required IEnumerable<JobExperience> ExperiencesUpdate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var experiencesUpdateBefore = writer.Position;
		var experiencesUpdateCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ExperiencesUpdate)
		{
			item.Serialize(writer);
			experiencesUpdateCount++;
		}
		var experiencesUpdateAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, experiencesUpdateBefore);
		writer.WriteInt16((short)experiencesUpdateCount);
		writer.Seek(SeekOrigin.Begin, experiencesUpdateAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var experiencesUpdateCount = reader.ReadInt16();
		var experiencesUpdate = new JobExperience[experiencesUpdateCount];
		for (var i = 0; i < experiencesUpdateCount; i++)
		{
			var entry = JobExperience.Empty;
			entry.Deserialize(reader);
			experiencesUpdate[i] = entry;
		}
		ExperiencesUpdate = experiencesUpdate;
	}
}

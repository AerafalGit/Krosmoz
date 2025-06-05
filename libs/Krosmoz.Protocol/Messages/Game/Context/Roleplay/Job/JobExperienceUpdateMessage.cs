// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobExperienceUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5654;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobExperienceUpdateMessage Empty =>
		new() { ExperiencesUpdate = JobExperience.Empty };

	public required JobExperience ExperiencesUpdate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		ExperiencesUpdate.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ExperiencesUpdate = JobExperience.Empty;
		ExperiencesUpdate.Deserialize(reader);
	}
}

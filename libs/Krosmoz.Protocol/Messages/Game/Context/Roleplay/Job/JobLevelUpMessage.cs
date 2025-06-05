// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobLevelUpMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5656;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobLevelUpMessage Empty =>
		new() { NewLevel = 0, JobsDescription = JobDescription.Empty };

	public required byte NewLevel { get; set; }

	public required JobDescription JobsDescription { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt8(NewLevel);
		JobsDescription.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		NewLevel = reader.ReadUInt8();
		JobsDescription = JobDescription.Empty;
		JobsDescription.Deserialize(reader);
	}
}

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

	public required sbyte NewLevel { get; set; }

	public required JobDescription JobsDescription { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(NewLevel);
		JobsDescription.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		NewLevel = reader.ReadSByte();
		JobsDescription = JobDescription.Empty;
		JobsDescription.Deserialize(reader);
	}
}

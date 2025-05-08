// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

public sealed class JobExperience : DofusType
{
	public new const ushort StaticProtocolId = 98;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static JobExperience Empty =>
		new() { JobId = 0, JobLevel = 0, JobXP = 0, JobXpLevelFloor = 0, JobXpNextLevelFloor = 0 };

	public required sbyte JobId { get; set; }

	public required sbyte JobLevel { get; set; }

	public required double JobXP { get; set; }

	public required double JobXpLevelFloor { get; set; }

	public required double JobXpNextLevelFloor { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(JobId);
		writer.WriteSByte(JobLevel);
		writer.WriteDouble(JobXP);
		writer.WriteDouble(JobXpLevelFloor);
		writer.WriteDouble(JobXpNextLevelFloor);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		JobId = reader.ReadSByte();
		JobLevel = reader.ReadSByte();
		JobXP = reader.ReadDouble();
		JobXpLevelFloor = reader.ReadDouble();
		JobXpNextLevelFloor = reader.ReadDouble();
	}
}

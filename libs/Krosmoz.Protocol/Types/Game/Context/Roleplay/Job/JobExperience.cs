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

	public required byte JobLevel { get; set; }

	public required ulong JobXP { get; set; }

	public required ulong JobXpLevelFloor { get; set; }

	public required ulong JobXpNextLevelFloor { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(JobId);
		writer.WriteUInt8(JobLevel);
		writer.WriteVarUInt64(JobXP);
		writer.WriteVarUInt64(JobXpLevelFloor);
		writer.WriteVarUInt64(JobXpNextLevelFloor);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		JobId = reader.ReadInt8();
		JobLevel = reader.ReadUInt8();
		JobXP = reader.ReadVarUInt64();
		JobXpLevelFloor = reader.ReadVarUInt64();
		JobXpNextLevelFloor = reader.ReadVarUInt64();
	}
}

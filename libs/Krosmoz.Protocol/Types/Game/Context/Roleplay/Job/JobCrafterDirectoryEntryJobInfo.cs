// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectoryEntryJobInfo : DofusType
{
	public new const ushort StaticProtocolId = 195;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectoryEntryJobInfo Empty =>
		new() { JobId = 0, JobLevel = 0, UserDefinedParams = 0, MinSlots = 0 };

	public required sbyte JobId { get; set; }

	public required sbyte JobLevel { get; set; }

	public required sbyte UserDefinedParams { get; set; }

	public required sbyte MinSlots { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(JobId);
		writer.WriteSByte(JobLevel);
		writer.WriteSByte(UserDefinedParams);
		writer.WriteSByte(MinSlots);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		JobId = reader.ReadSByte();
		JobLevel = reader.ReadSByte();
		UserDefinedParams = reader.ReadSByte();
		MinSlots = reader.ReadSByte();
	}
}

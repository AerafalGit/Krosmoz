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
		new() { JobId = 0, JobLevel = 0, Free = false, MinLevel = 0 };

	public required sbyte JobId { get; set; }

	public required byte JobLevel { get; set; }

	public required bool Free { get; set; }

	public required byte MinLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(JobId);
		writer.WriteUInt8(JobLevel);
		writer.WriteBoolean(Free);
		writer.WriteUInt8(MinLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		JobId = reader.ReadInt8();
		JobLevel = reader.ReadUInt8();
		Free = reader.ReadBoolean();
		MinLevel = reader.ReadUInt8();
	}
}

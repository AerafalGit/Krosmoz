// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectoryListEntry : DofusType
{
	public new const ushort StaticProtocolId = 196;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectoryListEntry Empty =>
		new() { PlayerInfo = JobCrafterDirectoryEntryPlayerInfo.Empty, JobInfo = JobCrafterDirectoryEntryJobInfo.Empty };

	public required JobCrafterDirectoryEntryPlayerInfo PlayerInfo { get; set; }

	public required JobCrafterDirectoryEntryJobInfo JobInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		PlayerInfo.Serialize(writer);
		JobInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PlayerInfo = JobCrafterDirectoryEntryPlayerInfo.Empty;
		PlayerInfo.Deserialize(reader);
		JobInfo = JobCrafterDirectoryEntryJobInfo.Empty;
		JobInfo.Deserialize(reader);
	}
}

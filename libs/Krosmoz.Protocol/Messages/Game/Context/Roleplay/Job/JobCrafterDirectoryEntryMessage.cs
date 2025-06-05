// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectoryEntryMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6044;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectoryEntryMessage Empty =>
		new() { PlayerInfo = JobCrafterDirectoryEntryPlayerInfo.Empty, JobInfoList = [], PlayerLook = EntityLook.Empty };

	public required JobCrafterDirectoryEntryPlayerInfo PlayerInfo { get; set; }

	public required IEnumerable<JobCrafterDirectoryEntryJobInfo> JobInfoList { get; set; }

	public required EntityLook PlayerLook { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		PlayerInfo.Serialize(writer);
		var jobInfoListBefore = writer.Position;
		var jobInfoListCount = 0;
		writer.WriteInt16(0);
		foreach (var item in JobInfoList)
		{
			item.Serialize(writer);
			jobInfoListCount++;
		}
		var jobInfoListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, jobInfoListBefore);
		writer.WriteInt16((short)jobInfoListCount);
		writer.Seek(SeekOrigin.Begin, jobInfoListAfter);
		PlayerLook.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PlayerInfo = JobCrafterDirectoryEntryPlayerInfo.Empty;
		PlayerInfo.Deserialize(reader);
		var jobInfoListCount = reader.ReadInt16();
		var jobInfoList = new JobCrafterDirectoryEntryJobInfo[jobInfoListCount];
		for (var i = 0; i < jobInfoListCount; i++)
		{
			var entry = JobCrafterDirectoryEntryJobInfo.Empty;
			entry.Deserialize(reader);
			jobInfoList[i] = entry;
		}
		JobInfoList = jobInfoList;
		PlayerLook = EntityLook.Empty;
		PlayerLook.Deserialize(reader);
	}
}

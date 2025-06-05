// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectorySettingsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5652;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectorySettingsMessage Empty =>
		new() { CraftersSettings = [] };

	public required IEnumerable<JobCrafterDirectorySettings> CraftersSettings { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var craftersSettingsBefore = writer.Position;
		var craftersSettingsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in CraftersSettings)
		{
			item.Serialize(writer);
			craftersSettingsCount++;
		}
		var craftersSettingsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, craftersSettingsBefore);
		writer.WriteInt16((short)craftersSettingsCount);
		writer.Seek(SeekOrigin.Begin, craftersSettingsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var craftersSettingsCount = reader.ReadInt16();
		var craftersSettings = new JobCrafterDirectorySettings[craftersSettingsCount];
		for (var i = 0; i < craftersSettingsCount; i++)
		{
			var entry = JobCrafterDirectorySettings.Empty;
			entry.Deserialize(reader);
			craftersSettings[i] = entry;
		}
		CraftersSettings = craftersSettings;
	}
}

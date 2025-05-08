// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectoryDefineSettingsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5649;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectoryDefineSettingsMessage Empty =>
		new() { Settings = JobCrafterDirectorySettings.Empty };

	public required JobCrafterDirectorySettings Settings { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Settings.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Settings = JobCrafterDirectorySettings.Empty;
		Settings.Deserialize(reader);
	}
}

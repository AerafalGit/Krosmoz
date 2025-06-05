// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectoryAddMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5651;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectoryAddMessage Empty =>
		new() { ListEntry = JobCrafterDirectoryListEntry.Empty };

	public required JobCrafterDirectoryListEntry ListEntry { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		ListEntry.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ListEntry = JobCrafterDirectoryListEntry.Empty;
		ListEntry.Deserialize(reader);
	}
}

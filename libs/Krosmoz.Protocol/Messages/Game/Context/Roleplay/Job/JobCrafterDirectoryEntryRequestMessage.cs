// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectoryEntryRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6043;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectoryEntryRequestMessage Empty =>
		new() { PlayerId = 0 };

	public required uint PlayerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(PlayerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PlayerId = reader.ReadVarUInt32();
	}
}

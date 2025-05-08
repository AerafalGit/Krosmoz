// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectoryRemoveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5653;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectoryRemoveMessage Empty =>
		new() { JobId = 0, PlayerId = 0 };

	public required sbyte JobId { get; set; }

	public required int PlayerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(JobId);
		writer.WriteInt(PlayerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		JobId = reader.ReadSByte();
		PlayerId = reader.ReadInt();
	}
}

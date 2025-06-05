// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobBookSubscriptionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6593;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobBookSubscriptionMessage Empty =>
		new() { AddedOrDeleted = false, JobId = 0 };

	public required bool AddedOrDeleted { get; set; }

	public required sbyte JobId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(AddedOrDeleted);
		writer.WriteInt8(JobId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AddedOrDeleted = reader.ReadBoolean();
		JobId = reader.ReadInt8();
	}
}

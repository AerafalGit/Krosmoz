// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartOkJobIndexMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5819;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeStartOkJobIndexMessage Empty =>
		new() { Jobs = [] };

	public required IEnumerable<int> Jobs { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var jobsBefore = writer.Position;
		var jobsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Jobs)
		{
			writer.WriteInt32(item);
			jobsCount++;
		}
		var jobsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, jobsBefore);
		writer.WriteInt16((short)jobsCount);
		writer.Seek(SeekOrigin.Begin, jobsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var jobsCount = reader.ReadInt16();
		var jobs = new int[jobsCount];
		for (var i = 0; i < jobsCount; i++)
		{
			jobs[i] = reader.ReadInt32();
		}
		Jobs = jobs;
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicLatencyStatsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5663;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicLatencyStatsMessage Empty =>
		new() { Latency = 0, SampleCount = 0, Max = 0 };

	public required ushort Latency { get; set; }

	public required short SampleCount { get; set; }

	public required short Max { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUShort(Latency);
		writer.WriteShort(SampleCount);
		writer.WriteShort(Max);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Latency = reader.ReadUShort();
		SampleCount = reader.ReadShort();
		Max = reader.ReadShort();
	}
}

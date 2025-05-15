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
		writer.WriteUInt16(Latency);
		writer.WriteInt16(SampleCount);
		writer.WriteInt16(Max);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Latency = reader.ReadUInt16();
		SampleCount = reader.ReadInt16();
		Max = reader.ReadInt16();
	}
}

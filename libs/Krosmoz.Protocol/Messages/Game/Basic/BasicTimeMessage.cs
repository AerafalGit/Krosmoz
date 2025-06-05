// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicTimeMessage : DofusMessage
{
	public new const uint StaticProtocolId = 175;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicTimeMessage Empty =>
		new() { Timestamp = 0, TimezoneOffset = 0 };

	public required double Timestamp { get; set; }

	public required short TimezoneOffset { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteDouble(Timestamp);
		writer.WriteInt16(TimezoneOffset);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Timestamp = reader.ReadDouble();
		TimezoneOffset = reader.ReadInt16();
	}
}

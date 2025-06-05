// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Almanach;

public sealed class AlmanachCalendarDateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6341;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AlmanachCalendarDateMessage Empty =>
		new() { Date = 0 };

	public required int Date { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(Date);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Date = reader.ReadInt32();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicDateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 177;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicDateMessage Empty =>
		new() { Day = 0, Month = 0, Year = 0 };

	public required sbyte Day { get; set; }

	public required sbyte Month { get; set; }

	public required short Year { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Day);
		writer.WriteInt8(Month);
		writer.WriteInt16(Year);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Day = reader.ReadInt8();
		Month = reader.ReadInt8();
		Year = reader.ReadInt16();
	}
}

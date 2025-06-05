// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items.Effects;

public sealed class ObjectEffectDate : ObjectEffect
{
	public new const ushort StaticProtocolId = 72;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectEffectDate Empty =>
		new() { ActionId = 0, Year = 0, Month = 0, Day = 0, Hour = 0, Minute = 0 };

	public required ushort Year { get; set; }

	public required sbyte Month { get; set; }

	public required sbyte Day { get; set; }

	public required sbyte Hour { get; set; }

	public required sbyte Minute { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(Year);
		writer.WriteInt8(Month);
		writer.WriteInt8(Day);
		writer.WriteInt8(Hour);
		writer.WriteInt8(Minute);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Year = reader.ReadVarUInt16();
		Month = reader.ReadInt8();
		Day = reader.ReadInt8();
		Hour = reader.ReadInt8();
		Minute = reader.ReadInt8();
	}
}

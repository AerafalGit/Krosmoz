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

	public required short Year { get; set; }

	public required short Month { get; set; }

	public required short Day { get; set; }

	public required short Hour { get; set; }

	public required short Minute { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(Year);
		writer.WriteShort(Month);
		writer.WriteShort(Day);
		writer.WriteShort(Hour);
		writer.WriteShort(Minute);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Year = reader.ReadShort();
		Month = reader.ReadShort();
		Day = reader.ReadShort();
		Hour = reader.ReadShort();
		Minute = reader.ReadShort();
	}
}

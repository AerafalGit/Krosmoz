// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items.Effects;

public sealed class ObjectEffectMinMax : ObjectEffect
{
	public new const ushort StaticProtocolId = 82;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectEffectMinMax Empty =>
		new() { ActionId = 0, Min = 0, Max = 0 };

	public required short Min { get; set; }

	public required short Max { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(Min);
		writer.WriteInt16(Max);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Min = reader.ReadInt16();
		Max = reader.ReadInt16();
	}
}

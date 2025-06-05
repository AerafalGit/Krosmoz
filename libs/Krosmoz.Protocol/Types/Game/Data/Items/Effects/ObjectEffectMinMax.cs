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

	public required uint Min { get; set; }

	public required uint Max { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Min);
		writer.WriteVarUInt32(Max);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Min = reader.ReadVarUInt32();
		Max = reader.ReadVarUInt32();
	}
}

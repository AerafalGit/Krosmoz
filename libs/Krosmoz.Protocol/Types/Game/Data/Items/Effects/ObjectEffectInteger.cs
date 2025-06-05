// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items.Effects;

public sealed class ObjectEffectInteger : ObjectEffect
{
	public new const ushort StaticProtocolId = 70;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectEffectInteger Empty =>
		new() { ActionId = 0, Value = 0 };

	public required ushort Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(Value);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Value = reader.ReadVarUInt16();
	}
}

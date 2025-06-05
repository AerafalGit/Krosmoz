// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items.Effects;

public sealed class ObjectEffectString : ObjectEffect
{
	public new const ushort StaticProtocolId = 74;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectEffectString Empty =>
		new() { ActionId = 0, Value = string.Empty };

	public required string Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(Value);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Value = reader.ReadUtfPrefixedLength16();
	}
}

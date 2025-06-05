// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items.Effects;

public sealed class ObjectEffectDuration : ObjectEffect
{
	public new const ushort StaticProtocolId = 75;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectEffectDuration Empty =>
		new() { ActionId = 0, Days = 0, Hours = 0, Minutes = 0 };

	public required ushort Days { get; set; }

	public required sbyte Hours { get; set; }

	public required sbyte Minutes { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(Days);
		writer.WriteInt8(Hours);
		writer.WriteInt8(Minutes);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Days = reader.ReadVarUInt16();
		Hours = reader.ReadInt8();
		Minutes = reader.ReadInt8();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Look;

public sealed class SubEntity : DofusType
{
	public new const ushort StaticProtocolId = 54;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static SubEntity Empty =>
		new() { BindingPointCategory = 0, BindingPointIndex = 0, SubEntityLook = EntityLook.Empty };

	public required sbyte BindingPointCategory { get; set; }

	public required sbyte BindingPointIndex { get; set; }

	public required EntityLook SubEntityLook { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(BindingPointCategory);
		writer.WriteInt8(BindingPointIndex);
		SubEntityLook.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		BindingPointCategory = reader.ReadInt8();
		BindingPointIndex = reader.ReadInt8();
		SubEntityLook = EntityLook.Empty;
		SubEntityLook.Deserialize(reader);
	}
}

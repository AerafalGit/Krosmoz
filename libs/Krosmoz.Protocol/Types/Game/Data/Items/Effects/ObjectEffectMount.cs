// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items.Effects;

public sealed class ObjectEffectMount : ObjectEffect
{
	public new const ushort StaticProtocolId = 179;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectEffectMount Empty =>
		new() { ActionId = 0, MountId = 0, Date = 0, ModelId = 0 };

	public required int MountId { get; set; }

	public required double Date { get; set; }

	public required ushort ModelId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(MountId);
		writer.WriteDouble(Date);
		writer.WriteVarUInt16(ModelId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MountId = reader.ReadInt32();
		Date = reader.ReadDouble();
		ModelId = reader.ReadVarUInt16();
	}
}

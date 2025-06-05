// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Look;

public sealed class IndexedEntityLook : DofusType
{
	public new const ushort StaticProtocolId = 405;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static IndexedEntityLook Empty =>
		new() { Look = EntityLook.Empty, Index = 0 };

	public required EntityLook Look { get; set; }

	public required sbyte Index { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Look.Serialize(writer);
		writer.WriteInt8(Index);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Look = EntityLook.Empty;
		Look.Deserialize(reader);
		Index = reader.ReadInt8();
	}
}

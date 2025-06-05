// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Mount;

public class UpdateMountBoost : DofusType
{
	public new const ushort StaticProtocolId = 356;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static UpdateMountBoost Empty =>
		new() { Type = 0 };

	public required sbyte Type { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Type);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Type = reader.ReadInt8();
	}
}

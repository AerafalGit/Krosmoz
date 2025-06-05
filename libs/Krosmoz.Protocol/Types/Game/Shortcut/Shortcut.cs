// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Shortcut;

public class Shortcut : DofusType
{
	public new const ushort StaticProtocolId = 369;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static Shortcut Empty =>
		new() { Slot = 0 };

	public required sbyte Slot { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Slot);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Slot = reader.ReadInt8();
	}
}

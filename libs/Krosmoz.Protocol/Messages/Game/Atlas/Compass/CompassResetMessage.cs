// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Atlas.Compass;

public sealed class CompassResetMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5584;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CompassResetMessage Empty =>
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

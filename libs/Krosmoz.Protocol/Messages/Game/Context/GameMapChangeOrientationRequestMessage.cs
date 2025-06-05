// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameMapChangeOrientationRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 945;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameMapChangeOrientationRequestMessage Empty =>
		new() { Direction = 0 };

	public required sbyte Direction { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Direction);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Direction = reader.ReadInt8();
	}
}

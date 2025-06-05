// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context;

public sealed class ActorOrientation : DofusType
{
	public new const ushort StaticProtocolId = 353;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static ActorOrientation Empty =>
		new() { Id = 0, Direction = 0 };

	public required int Id { get; set; }

	public required sbyte Direction { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(Id);
		writer.WriteInt8(Direction);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadInt32();
		Direction = reader.ReadInt8();
	}
}

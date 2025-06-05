// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Status;

public class PlayerStatus : DofusType
{
	public new const ushort StaticProtocolId = 415;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static PlayerStatus Empty =>
		new() { StatusId = 0 };

	public required sbyte StatusId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(StatusId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		StatusId = reader.ReadInt8();
	}
}

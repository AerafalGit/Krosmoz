// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Status;

public sealed class PlayerStatusExtended : PlayerStatus
{
	public new const ushort StaticProtocolId = 414;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PlayerStatusExtended Empty =>
		new() { StatusId = 0, Message = string.Empty };

	public required string Message { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(Message);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Message = reader.ReadUtfPrefixedLength16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection;

public sealed class SelectedServerRefusedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 41;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SelectedServerRefusedMessage Empty =>
		new() { ServerId = 0, Error = 0, ServerStatus = 0 };

	public required ushort ServerId { get; set; }

	public required sbyte Error { get; set; }

	public required sbyte ServerStatus { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ServerId);
		writer.WriteInt8(Error);
		writer.WriteInt8(ServerStatus);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ServerId = reader.ReadVarUInt16();
		Error = reader.ReadInt8();
		ServerStatus = reader.ReadInt8();
	}
}

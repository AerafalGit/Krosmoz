// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection;

public sealed class ServerSelectionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 40;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ServerSelectionMessage Empty =>
		new() { ServerId = 0 };

	public required ushort ServerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ServerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ServerId = reader.ReadVarUInt16();
	}
}

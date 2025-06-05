// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection;

public class SelectedServerDataMessage : DofusMessage
{
	public new const uint StaticProtocolId = 42;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SelectedServerDataMessage Empty =>
		new() { ServerId = 0, Address = string.Empty, Port = 0, CanCreateNewCharacter = false, Ticket = [] };

	public required ushort ServerId { get; set; }

	public required string Address { get; set; }

	public required ushort Port { get; set; }

	public required bool CanCreateNewCharacter { get; set; }

	public required IEnumerable<sbyte> Ticket { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ServerId);
		writer.WriteUtfPrefixedLength16(Address);
		writer.WriteUInt16(Port);
		writer.WriteBoolean(CanCreateNewCharacter);
		var ticketBefore = writer.Position;
		var ticketCount = 0;
		writer.WriteVarInt32(0);
		foreach (var item in Ticket)
		{
			writer.WriteInt8(item);
			ticketCount++;
		}
		var ticketAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, ticketBefore);
		writer.WriteVarInt32(ticketCount);
		writer.Seek(SeekOrigin.Begin, ticketAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ServerId = reader.ReadVarUInt16();
		Address = reader.ReadUtfPrefixedLength16();
		Port = reader.ReadUInt16();
		CanCreateNewCharacter = reader.ReadBoolean();
		var ticketCount = reader.ReadVarInt32();
		var ticket = new sbyte[ticketCount];
		for (var i = 0; i < ticketCount; i++)
		{
			ticket[i] = reader.ReadInt8();
		}
		Ticket = ticket;
	}
}

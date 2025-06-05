// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight.Arena;

public sealed class GameRolePlayArenaSwitchToFightServerMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6575;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayArenaSwitchToFightServerMessage Empty =>
		new() { Address = string.Empty, Port = 0, Ticket = [] };

	public required string Address { get; set; }

	public required ushort Port { get; set; }

	public required IEnumerable<sbyte> Ticket { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Address);
		writer.WriteUInt16(Port);
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
		Address = reader.ReadUtfPrefixedLength16();
		Port = reader.ReadUInt16();
		var ticketCount = reader.ReadVarInt32();
		var ticket = new sbyte[ticketCount];
		for (var i = 0; i < ticketCount; i++)
		{
			ticket[i] = reader.ReadInt8();
		}
		Ticket = ticket;
	}
}

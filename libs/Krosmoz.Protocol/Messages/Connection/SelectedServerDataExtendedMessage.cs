// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection;

public sealed class SelectedServerDataExtendedMessage : SelectedServerDataMessage
{
	public new const uint StaticProtocolId = 6469;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new SelectedServerDataExtendedMessage Empty =>
		new() { Ticket = [], CanCreateNewCharacter = false, Port = 0, Address = string.Empty, ServerId = 0, ServerIds = [] };

	public required IEnumerable<ushort> ServerIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var serverIdsBefore = writer.Position;
		var serverIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ServerIds)
		{
			writer.WriteVarUInt16(item);
			serverIdsCount++;
		}
		var serverIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, serverIdsBefore);
		writer.WriteInt16((short)serverIdsCount);
		writer.Seek(SeekOrigin.Begin, serverIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var serverIdsCount = reader.ReadInt16();
		var serverIds = new ushort[serverIdsCount];
		for (var i = 0; i < serverIdsCount; i++)
		{
			serverIds[i] = reader.ReadVarUInt16();
		}
		ServerIds = serverIds;
	}
}

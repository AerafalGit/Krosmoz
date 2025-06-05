// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Connection;

namespace Krosmoz.Protocol.Messages.Connection;

public sealed class ServersListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 30;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ServersListMessage Empty =>
		new() { Servers = [], AlreadyConnectedToServerId = 0, CanCreateNewCharacter = false };

	public required IEnumerable<GameServerInformations> Servers { get; set; }

	public required ushort AlreadyConnectedToServerId { get; set; }

	public required bool CanCreateNewCharacter { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var serversBefore = writer.Position;
		var serversCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Servers)
		{
			item.Serialize(writer);
			serversCount++;
		}
		var serversAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, serversBefore);
		writer.WriteInt16((short)serversCount);
		writer.Seek(SeekOrigin.Begin, serversAfter);
		writer.WriteVarUInt16(AlreadyConnectedToServerId);
		writer.WriteBoolean(CanCreateNewCharacter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var serversCount = reader.ReadInt16();
		var servers = new GameServerInformations[serversCount];
		for (var i = 0; i < serversCount; i++)
		{
			var entry = GameServerInformations.Empty;
			entry.Deserialize(reader);
			servers[i] = entry;
		}
		Servers = servers;
		AlreadyConnectedToServerId = reader.ReadVarUInt16();
		CanCreateNewCharacter = reader.ReadBoolean();
	}
}

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
		new() { Servers = [] };

	public required IEnumerable<GameServerInformations> Servers { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var serversBefore = writer.Position;
		var serversCount = 0;
		writer.WriteShort(0);
		foreach (var item in Servers)
		{
			item.Serialize(writer);
			serversCount++;
		}
		var serversAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, serversBefore);
		writer.WriteShort((short)serversCount);
		writer.Seek(SeekOrigin.Begin, serversAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var serversCount = reader.ReadShort();
		var servers = new GameServerInformations[serversCount];
		for (var i = 0; i < serversCount; i++)
		{
			var entry = GameServerInformations.Empty;
			entry.Deserialize(reader);
			servers[i] = entry;
		}
		Servers = servers;
	}
}

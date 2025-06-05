// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection.Search;

public sealed class AcquaintanceServerListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6142;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AcquaintanceServerListMessage Empty =>
		new() { Servers = [] };

	public required IEnumerable<ushort> Servers { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var serversBefore = writer.Position;
		var serversCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Servers)
		{
			writer.WriteVarUInt16(item);
			serversCount++;
		}
		var serversAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, serversBefore);
		writer.WriteInt16((short)serversCount);
		writer.Seek(SeekOrigin.Begin, serversAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var serversCount = reader.ReadInt16();
		var servers = new ushort[serversCount];
		for (var i = 0; i < serversCount; i++)
		{
			servers[i] = reader.ReadVarUInt16();
		}
		Servers = servers;
	}
}

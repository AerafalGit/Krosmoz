// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Connection;

namespace Krosmoz.Protocol.Messages.Connection;

public sealed class ServerStatusUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 50;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ServerStatusUpdateMessage Empty =>
		new() { Server = GameServerInformations.Empty };

	public required GameServerInformations Server { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Server.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Server = GameServerInformations.Empty;
		Server.Deserialize(reader);
	}
}

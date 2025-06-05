// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Initialization;

public sealed class OnConnectionEventMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5726;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static OnConnectionEventMessage Empty =>
		new() { EventType = 0 };

	public required sbyte EventType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(EventType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		EventType = reader.ReadInt8();
	}
}

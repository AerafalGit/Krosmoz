// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class CurrentServerStatusUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6525;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CurrentServerStatusUpdateMessage Empty =>
		new() { Status = 0 };

	public required sbyte Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Status);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Status = reader.ReadInt8();
	}
}

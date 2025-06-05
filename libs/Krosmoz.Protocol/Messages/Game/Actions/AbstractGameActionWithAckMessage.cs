// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions;

public sealed class AbstractGameActionWithAckMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 1001;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new AbstractGameActionWithAckMessage Empty =>
		new() { SourceId = 0, ActionId = 0, WaitAckId = 0 };

	public required short WaitAckId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(WaitAckId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		WaitAckId = reader.ReadInt16();
	}
}

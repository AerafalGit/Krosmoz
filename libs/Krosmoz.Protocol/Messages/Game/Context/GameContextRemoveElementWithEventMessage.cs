// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameContextRemoveElementWithEventMessage : GameContextRemoveElementMessage
{
	public new const uint StaticProtocolId = 6412;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameContextRemoveElementWithEventMessage Empty =>
		new() { Id = 0, ElementEventId = 0 };

	public required sbyte ElementEventId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(ElementEventId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ElementEventId = reader.ReadInt8();
	}
}

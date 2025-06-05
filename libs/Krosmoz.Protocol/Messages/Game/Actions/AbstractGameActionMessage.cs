// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions;

public class AbstractGameActionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1000;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AbstractGameActionMessage Empty =>
		new() { ActionId = 0, SourceId = 0 };

	public required ushort ActionId { get; set; }

	public required int SourceId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ActionId);
		writer.WriteInt32(SourceId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ActionId = reader.ReadVarUInt16();
		SourceId = reader.ReadInt32();
	}
}

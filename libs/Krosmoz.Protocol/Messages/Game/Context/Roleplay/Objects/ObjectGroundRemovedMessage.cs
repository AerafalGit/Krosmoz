// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Objects;

public sealed class ObjectGroundRemovedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3014;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectGroundRemovedMessage Empty =>
		new() { Cell = 0 };

	public required ushort Cell { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Cell);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Cell = reader.ReadVarUInt16();
	}
}

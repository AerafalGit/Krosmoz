// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public sealed class TeleportOnSameMapMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6048;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TeleportOnSameMapMessage Empty =>
		new() { TargetId = 0, CellId = 0 };

	public required int TargetId { get; set; }

	public required ushort CellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(TargetId);
		writer.WriteVarUInt16(CellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TargetId = reader.ReadInt32();
		CellId = reader.ReadVarUInt16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Lockable;

public sealed class LockableStateUpdateStorageMessage : LockableStateUpdateAbstractMessage
{
	public new const uint StaticProtocolId = 5669;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new LockableStateUpdateStorageMessage Empty =>
		new() { Locked = false, MapId = 0, ElementId = 0 };

	public required int MapId { get; set; }

	public required uint ElementId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(MapId);
		writer.WriteVarUInt32(ElementId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MapId = reader.ReadInt32();
		ElementId = reader.ReadVarUInt32();
	}
}

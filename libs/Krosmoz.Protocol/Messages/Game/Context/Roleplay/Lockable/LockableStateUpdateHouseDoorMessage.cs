// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Lockable;

public sealed class LockableStateUpdateHouseDoorMessage : LockableStateUpdateAbstractMessage
{
	public new const uint StaticProtocolId = 5668;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new LockableStateUpdateHouseDoorMessage Empty =>
		new() { Locked = false, HouseId = 0 };

	public required int HouseId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(HouseId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		HouseId = reader.ReadInt32();
	}
}

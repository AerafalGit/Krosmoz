// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Delay;

public sealed class GameRolePlayDelayedObjectUseMessage : GameRolePlayDelayedActionMessage
{
	public new const uint StaticProtocolId = 6425;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayDelayedObjectUseMessage Empty =>
		new() { DelayEndTime = 0, DelayTypeId = 0, DelayedCharacterId = 0, ObjectGID = 0 };

	public required ushort ObjectGID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(ObjectGID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ObjectGID = reader.ReadVarUInt16();
	}
}

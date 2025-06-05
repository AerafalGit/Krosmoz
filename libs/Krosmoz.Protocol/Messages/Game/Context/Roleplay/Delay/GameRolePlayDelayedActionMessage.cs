// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Delay;

public class GameRolePlayDelayedActionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6153;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayDelayedActionMessage Empty =>
		new() { DelayedCharacterId = 0, DelayTypeId = 0, DelayEndTime = 0 };

	public required int DelayedCharacterId { get; set; }

	public required sbyte DelayTypeId { get; set; }

	public required double DelayEndTime { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(DelayedCharacterId);
		writer.WriteInt8(DelayTypeId);
		writer.WriteDouble(DelayEndTime);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DelayedCharacterId = reader.ReadInt32();
		DelayTypeId = reader.ReadInt8();
		DelayEndTime = reader.ReadDouble();
	}
}

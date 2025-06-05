// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Delay;

public sealed class GameRolePlayDelayedActionFinishedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6150;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayDelayedActionFinishedMessage Empty =>
		new() { DelayedCharacterId = 0, DelayTypeId = 0 };

	public required int DelayedCharacterId { get; set; }

	public required sbyte DelayTypeId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(DelayedCharacterId);
		writer.WriteInt8(DelayTypeId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DelayedCharacterId = reader.ReadInt32();
		DelayTypeId = reader.ReadInt8();
	}
}

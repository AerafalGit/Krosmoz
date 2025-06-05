// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public class GameFightTurnStartMessage : DofusMessage
{
	public new const uint StaticProtocolId = 714;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightTurnStartMessage Empty =>
		new() { Id = 0, WaitTime = 0 };

	public required int Id { get; set; }

	public required uint WaitTime { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(Id);
		writer.WriteVarUInt32(WaitTime);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadInt32();
		WaitTime = reader.ReadVarUInt32();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightTurnEndMessage : DofusMessage
{
	public new const uint StaticProtocolId = 719;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightTurnEndMessage Empty =>
		new() { Id = 0 };

	public required int Id { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(Id);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadInt32();
	}
}

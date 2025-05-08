// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightTurnReadyMessage : DofusMessage
{
	public new const uint StaticProtocolId = 716;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightTurnReadyMessage Empty =>
		new() { IsReady = false };

	public required bool IsReady { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(IsReady);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		IsReady = reader.ReadBoolean();
	}
}

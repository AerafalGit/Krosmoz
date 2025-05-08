// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightNewRoundMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6239;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightNewRoundMessage Empty =>
		new() { RoundNumber = 0 };

	public required int RoundNumber { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(RoundNumber);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RoundNumber = reader.ReadInt();
	}
}

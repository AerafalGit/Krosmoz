// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightStartingMessage : DofusMessage
{
	public new const uint StaticProtocolId = 700;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightStartingMessage Empty =>
		new() { FightType = 0 };

	public required sbyte FightType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(FightType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightType = reader.ReadSByte();
	}
}

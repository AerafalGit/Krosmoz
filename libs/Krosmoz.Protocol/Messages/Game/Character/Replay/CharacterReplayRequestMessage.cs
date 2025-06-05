// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Replay;

public class CharacterReplayRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 167;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterReplayRequestMessage Empty =>
		new() { CharacterId = 0 };

	public required int CharacterId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(CharacterId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CharacterId = reader.ReadInt32();
	}
}

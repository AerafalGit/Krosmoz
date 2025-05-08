// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Replay;

public sealed class CharacterReplayWithRelookRequestMessage : CharacterReplayRequestMessage
{
	public new const uint StaticProtocolId = 6354;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharacterReplayWithRelookRequestMessage Empty =>
		new() { CharacterId = 0, CosmeticId = 0 };

	public required int CosmeticId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(CosmeticId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CosmeticId = reader.ReadInt();
	}
}

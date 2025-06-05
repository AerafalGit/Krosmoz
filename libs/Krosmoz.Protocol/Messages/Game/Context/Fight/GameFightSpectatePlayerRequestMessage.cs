// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightSpectatePlayerRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6474;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightSpectatePlayerRequestMessage Empty =>
		new() { PlayerId = 0 };

	public required int PlayerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(PlayerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PlayerId = reader.ReadInt32();
	}
}

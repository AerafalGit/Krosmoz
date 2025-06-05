// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameContextReadyMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6071;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameContextReadyMessage Empty =>
		new() { MapId = 0 };

	public required int MapId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(MapId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MapId = reader.ReadInt32();
	}
}

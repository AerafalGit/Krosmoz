// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive.Zaap;

public sealed class ZaapRespawnUpdatedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6571;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ZaapRespawnUpdatedMessage Empty =>
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

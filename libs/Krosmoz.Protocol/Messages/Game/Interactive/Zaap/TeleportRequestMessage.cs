// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive.Zaap;

public sealed class TeleportRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5961;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TeleportRequestMessage Empty =>
		new() { TeleporterType = 0, MapId = 0 };

	public required sbyte TeleporterType { get; set; }

	public required int MapId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(TeleporterType);
		writer.WriteInt32(MapId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TeleporterType = reader.ReadInt8();
		MapId = reader.ReadInt32();
	}
}

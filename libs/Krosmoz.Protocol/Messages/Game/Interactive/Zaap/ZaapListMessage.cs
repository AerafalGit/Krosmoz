// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive.Zaap;

public sealed class ZaapListMessage : TeleportDestinationsListMessage
{
	public new const uint StaticProtocolId = 1604;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ZaapListMessage Empty =>
		new() { DestTeleporterType = [], Costs = [], SubAreaIds = [], MapIds = [], TeleporterType = 0, SpawnMapId = 0 };

	public required int SpawnMapId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(SpawnMapId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SpawnMapId = reader.ReadInt32();
	}
}

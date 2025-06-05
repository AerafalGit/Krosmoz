// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class GuildFightTakePlaceRequestMessage : GuildFightJoinRequestMessage
{
	public new const uint StaticProtocolId = 6235;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GuildFightTakePlaceRequestMessage Empty =>
		new() { TaxCollectorId = 0, ReplacedCharacterId = 0 };

	public required int ReplacedCharacterId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(ReplacedCharacterId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ReplacedCharacterId = reader.ReadInt32();
	}
}

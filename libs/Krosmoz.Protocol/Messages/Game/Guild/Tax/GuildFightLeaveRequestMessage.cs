// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class GuildFightLeaveRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5715;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildFightLeaveRequestMessage Empty =>
		new() { TaxCollectorId = 0, CharacterId = 0 };

	public required int TaxCollectorId { get; set; }

	public required int CharacterId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(TaxCollectorId);
		writer.WriteInt(CharacterId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TaxCollectorId = reader.ReadInt();
		CharacterId = reader.ReadInt();
	}
}

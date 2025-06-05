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

	public required uint CharacterId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(TaxCollectorId);
		writer.WriteVarUInt32(CharacterId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TaxCollectorId = reader.ReadInt32();
		CharacterId = reader.ReadVarUInt32();
	}
}

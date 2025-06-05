// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class GuildFightPlayersHelpersLeaveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5719;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildFightPlayersHelpersLeaveMessage Empty =>
		new() { FightId = 0, PlayerId = 0 };

	public required int FightId { get; set; }

	public required uint PlayerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(FightId);
		writer.WriteVarUInt32(PlayerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt32();
		PlayerId = reader.ReadVarUInt32();
	}
}

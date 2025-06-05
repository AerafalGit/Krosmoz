// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character;

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class GuildFightPlayersHelpersJoinMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5720;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildFightPlayersHelpersJoinMessage Empty =>
		new() { FightId = 0, PlayerInfo = CharacterMinimalPlusLookInformations.Empty };

	public required int FightId { get; set; }

	public required CharacterMinimalPlusLookInformations PlayerInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(FightId);
		PlayerInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt32();
		PlayerInfo = CharacterMinimalPlusLookInformations.Empty;
		PlayerInfo.Deserialize(reader);
	}
}

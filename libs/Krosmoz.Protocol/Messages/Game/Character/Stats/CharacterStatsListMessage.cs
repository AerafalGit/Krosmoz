// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Characteristic;

namespace Krosmoz.Protocol.Messages.Game.Character.Stats;

public sealed class CharacterStatsListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 500;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterStatsListMessage Empty =>
		new() { Stats = CharacterCharacteristicsInformations.Empty };

	public required CharacterCharacteristicsInformations Stats { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Stats.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Stats = CharacterCharacteristicsInformations.Empty;
		Stats.Deserialize(reader);
	}
}

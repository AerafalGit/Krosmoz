// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightSummonMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 5825;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightSummonMessage Empty =>
		new() { SourceId = 0, ActionId = 0, Summon = GameFightFighterInformations.Empty };

	public required GameFightFighterInformations Summon { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUShort(Summon.ProtocolId);
		Summon.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Summon = Types.TypeFactory.CreateType<GameFightFighterInformations>(reader.ReadUShort());
		Summon.Deserialize(reader);
	}
}

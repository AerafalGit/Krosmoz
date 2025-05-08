// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class GameFightFighterInformations : GameContextActorInformations
{
	public new const ushort StaticProtocolId = 143;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightFighterInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, TeamId = 0, Alive = false, Stats = GameFightMinimalStats.Empty };

	public required sbyte TeamId { get; set; }

	public required bool Alive { get; set; }

	public required GameFightMinimalStats Stats { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteSByte(TeamId);
		writer.WriteBoolean(Alive);
		writer.WriteUShort(Stats.ProtocolId);
		Stats.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TeamId = reader.ReadSByte();
		Alive = reader.ReadBoolean();
		Stats = Types.TypeFactory.CreateType<GameFightMinimalStats>(reader.ReadUShort());
		Stats.Deserialize(reader);
	}
}

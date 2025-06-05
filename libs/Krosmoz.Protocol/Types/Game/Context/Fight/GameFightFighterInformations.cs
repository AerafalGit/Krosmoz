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
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, TeamId = 0, Wave = 0, Alive = false, Stats = GameFightMinimalStats.Empty, PreviousPositions = [] };

	public required sbyte TeamId { get; set; }

	public required sbyte Wave { get; set; }

	public required bool Alive { get; set; }

	public required GameFightMinimalStats Stats { get; set; }

	public required IEnumerable<ushort> PreviousPositions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(TeamId);
		writer.WriteInt8(Wave);
		writer.WriteBoolean(Alive);
		writer.WriteUInt16(Stats.ProtocolId);
		Stats.Serialize(writer);
		var previousPositionsBefore = writer.Position;
		var previousPositionsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PreviousPositions)
		{
			writer.WriteVarUInt16(item);
			previousPositionsCount++;
		}
		var previousPositionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, previousPositionsBefore);
		writer.WriteInt16((short)previousPositionsCount);
		writer.Seek(SeekOrigin.Begin, previousPositionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TeamId = reader.ReadInt8();
		Wave = reader.ReadInt8();
		Alive = reader.ReadBoolean();
		Stats = Types.TypeFactory.CreateType<GameFightMinimalStats>(reader.ReadUInt16());
		Stats.Deserialize(reader);
		var previousPositionsCount = reader.ReadInt16();
		var previousPositions = new ushort[previousPositionsCount];
		for (var i = 0; i < previousPositionsCount; i++)
		{
			previousPositions[i] = reader.ReadVarUInt16();
		}
		PreviousPositions = previousPositions;
	}
}

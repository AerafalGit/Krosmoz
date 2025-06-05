// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class GameFightMonsterInformations : GameFightAIInformations
{
	public new const ushort StaticProtocolId = 29;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightMonsterInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, PreviousPositions = [], Stats = GameFightMinimalStats.Empty, Alive = false, Wave = 0, TeamId = 0, CreatureGenericId = 0, CreatureGrade = 0 };

	public required ushort CreatureGenericId { get; set; }

	public required sbyte CreatureGrade { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(CreatureGenericId);
		writer.WriteInt8(CreatureGrade);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CreatureGenericId = reader.ReadVarUInt16();
		CreatureGrade = reader.ReadInt8();
	}
}

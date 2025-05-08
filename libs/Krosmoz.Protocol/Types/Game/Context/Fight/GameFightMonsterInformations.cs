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
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Stats = GameFightMinimalStats.Empty, Alive = false, TeamId = 0, CreatureGenericId = 0, CreatureGrade = 0 };

	public required short CreatureGenericId { get; set; }

	public required sbyte CreatureGrade { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(CreatureGenericId);
		writer.WriteSByte(CreatureGrade);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CreatureGenericId = reader.ReadShort();
		CreatureGrade = reader.ReadSByte();
	}
}

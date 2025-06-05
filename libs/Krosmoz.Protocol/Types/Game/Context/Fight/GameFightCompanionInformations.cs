// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightCompanionInformations : GameFightFighterInformations
{
	public new const ushort StaticProtocolId = 450;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightCompanionInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, PreviousPositions = [], Stats = GameFightMinimalStats.Empty, Alive = false, Wave = 0, TeamId = 0, CompanionGenericId = 0, Level = 0, MasterId = 0 };

	public required sbyte CompanionGenericId { get; set; }

	public required byte Level { get; set; }

	public required int MasterId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(CompanionGenericId);
		writer.WriteUInt8(Level);
		writer.WriteInt32(MasterId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CompanionGenericId = reader.ReadInt8();
		Level = reader.ReadUInt8();
		MasterId = reader.ReadInt32();
	}
}

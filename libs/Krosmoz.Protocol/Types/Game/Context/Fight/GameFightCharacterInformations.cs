// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Alignment;
using Krosmoz.Protocol.Types.Game.Character.Status;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightCharacterInformations : GameFightFighterNamedInformations
{
	public new const ushort StaticProtocolId = 46;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightCharacterInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, PreviousPositions = [], Stats = GameFightMinimalStats.Empty, Alive = false, Wave = 0, TeamId = 0, Status = PlayerStatus.Empty, Name = string.Empty, Level = 0, AlignmentInfos = ActorAlignmentInformations.Empty, Breed = 0, Sex = false };

	public required byte Level { get; set; }

	public required ActorAlignmentInformations AlignmentInfos { get; set; }

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt8(Level);
		AlignmentInfos.Serialize(writer);
		writer.WriteInt8(Breed);
		writer.WriteBoolean(Sex);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Level = reader.ReadUInt8();
		AlignmentInfos = ActorAlignmentInformations.Empty;
		AlignmentInfos.Deserialize(reader);
		Breed = reader.ReadInt8();
		Sex = reader.ReadBoolean();
	}
}

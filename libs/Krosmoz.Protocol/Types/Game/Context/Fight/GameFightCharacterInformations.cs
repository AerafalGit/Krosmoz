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
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Stats = GameFightMinimalStats.Empty, Alive = false, TeamId = 0, Status = PlayerStatus.Empty, Name = string.Empty, Level = 0, AlignmentInfos = ActorAlignmentInformations.Empty, Breed = 0 };

	public required short Level { get; set; }

	public required ActorAlignmentInformations AlignmentInfos { get; set; }

	public required sbyte Breed { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(Level);
		AlignmentInfos.Serialize(writer);
		writer.WriteSByte(Breed);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Level = reader.ReadShort();
		AlignmentInfos = ActorAlignmentInformations.Empty;
		AlignmentInfos.Deserialize(reader);
		Breed = reader.ReadSByte();
	}
}

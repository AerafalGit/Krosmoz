// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Status;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightMutantInformations : GameFightFighterNamedInformations
{
	public new const ushort StaticProtocolId = 50;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightMutantInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, PreviousPositions = [], Stats = GameFightMinimalStats.Empty, Alive = false, Wave = 0, TeamId = 0, Status = PlayerStatus.Empty, Name = string.Empty, PowerLevel = 0 };

	public required sbyte PowerLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(PowerLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PowerLevel = reader.ReadInt8();
	}
}

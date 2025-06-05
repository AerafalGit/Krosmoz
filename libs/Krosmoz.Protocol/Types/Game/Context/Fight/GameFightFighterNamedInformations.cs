// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Status;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class GameFightFighterNamedInformations : GameFightFighterInformations
{
	public new const ushort StaticProtocolId = 158;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightFighterNamedInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, PreviousPositions = [], Stats = GameFightMinimalStats.Empty, Alive = false, Wave = 0, TeamId = 0, Name = string.Empty, Status = PlayerStatus.Empty };

	public required string Name { get; set; }

	public required PlayerStatus Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(Name);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Name = reader.ReadUtfPrefixedLength16();
		Status = PlayerStatus.Empty;
		Status.Deserialize(reader);
	}
}

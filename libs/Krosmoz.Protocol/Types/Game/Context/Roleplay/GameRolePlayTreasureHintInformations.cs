// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GameRolePlayTreasureHintInformations : GameRolePlayActorInformations
{
	public new const ushort StaticProtocolId = 471;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayTreasureHintInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, NpcId = 0 };

	public required ushort NpcId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(NpcId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		NpcId = reader.ReadVarUInt16();
	}
}

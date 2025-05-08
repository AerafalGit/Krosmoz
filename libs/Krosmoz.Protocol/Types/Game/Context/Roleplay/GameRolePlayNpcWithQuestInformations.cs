// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Quest;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GameRolePlayNpcWithQuestInformations : GameRolePlayNpcInformations
{
	public new const ushort StaticProtocolId = 383;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayNpcWithQuestInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, SpecialArtworkId = 0, Sex = false, NpcId = 0, QuestFlag = GameRolePlayNpcQuestFlag.Empty };

	public required GameRolePlayNpcQuestFlag QuestFlag { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		QuestFlag.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		QuestFlag = GameRolePlayNpcQuestFlag.Empty;
		QuestFlag.Deserialize(reader);
	}
}

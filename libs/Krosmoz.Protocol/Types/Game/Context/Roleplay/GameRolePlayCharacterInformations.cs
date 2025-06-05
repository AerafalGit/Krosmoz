// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Alignment;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GameRolePlayCharacterInformations : GameRolePlayHumanoidInformations
{
	public new const ushort StaticProtocolId = 36;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayCharacterInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Name = string.Empty, AccountId = 0, HumanoidInfo = HumanInformations.Empty, AlignmentInfos = ActorAlignmentInformations.Empty };

	public required ActorAlignmentInformations AlignmentInfos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		AlignmentInfos.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AlignmentInfos = ActorAlignmentInformations.Empty;
		AlignmentInfos.Deserialize(reader);
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight;

public sealed class GameRolePlayShowChallengeMessage : DofusMessage
{
	public new const uint StaticProtocolId = 301;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayShowChallengeMessage Empty =>
		new() { CommonsInfos = FightCommonInformations.Empty };

	public required FightCommonInformations CommonsInfos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		CommonsInfos.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CommonsInfos = FightCommonInformations.Empty;
		CommonsInfos.Deserialize(reader);
	}
}

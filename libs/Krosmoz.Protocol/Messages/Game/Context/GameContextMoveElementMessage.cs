// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameContextMoveElementMessage : DofusMessage
{
	public new const uint StaticProtocolId = 253;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameContextMoveElementMessage Empty =>
		new() { Movement = EntityMovementInformations.Empty };

	public required EntityMovementInformations Movement { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Movement.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Movement = EntityMovementInformations.Empty;
		Movement.Deserialize(reader);
	}
}

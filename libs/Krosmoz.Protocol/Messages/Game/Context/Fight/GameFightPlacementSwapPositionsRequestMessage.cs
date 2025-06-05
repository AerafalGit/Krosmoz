// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightPlacementSwapPositionsRequestMessage : GameFightPlacementPositionRequestMessage
{
	public new const uint StaticProtocolId = 6541;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameFightPlacementSwapPositionsRequestMessage Empty =>
		new() { CellId = 0, RequestedId = 0 };

	public required int RequestedId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(RequestedId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		RequestedId = reader.ReadInt32();
	}
}

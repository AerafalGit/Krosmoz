// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightPlacementSwapPositionsCancelledMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6546;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightPlacementSwapPositionsCancelledMessage Empty =>
		new() { RequestId = 0, CancellerId = 0 };

	public required int RequestId { get; set; }

	public required uint CancellerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(RequestId);
		writer.WriteVarUInt32(CancellerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RequestId = reader.ReadInt32();
		CancellerId = reader.ReadVarUInt32();
	}
}

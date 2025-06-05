// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightPlacementSwapPositionsOfferMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6542;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightPlacementSwapPositionsOfferMessage Empty =>
		new() { RequestId = 0, RequesterId = 0, RequesterCellId = 0, RequestedId = 0, RequestedCellId = 0 };

	public required int RequestId { get; set; }

	public required uint RequesterId { get; set; }

	public required ushort RequesterCellId { get; set; }

	public required uint RequestedId { get; set; }

	public required ushort RequestedCellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(RequestId);
		writer.WriteVarUInt32(RequesterId);
		writer.WriteVarUInt16(RequesterCellId);
		writer.WriteVarUInt32(RequestedId);
		writer.WriteVarUInt16(RequestedCellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RequestId = reader.ReadInt32();
		RequesterId = reader.ReadVarUInt32();
		RequesterCellId = reader.ReadVarUInt16();
		RequestedId = reader.ReadVarUInt32();
		RequestedCellId = reader.ReadVarUInt16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeHandleMountStableMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5965;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeHandleMountStableMessage Empty =>
		new() { ActionType = 0, RideId = 0 };

	public required sbyte ActionType { get; set; }

	public required int RideId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(ActionType);
		writer.WriteInt(RideId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ActionType = reader.ReadSByte();
		RideId = reader.ReadInt();
	}
}

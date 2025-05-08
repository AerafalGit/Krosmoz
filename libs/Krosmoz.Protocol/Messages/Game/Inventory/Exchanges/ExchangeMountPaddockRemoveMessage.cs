// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeMountPaddockRemoveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6050;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeMountPaddockRemoveMessage Empty =>
		new() { MountId = 0 };

	public required double MountId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteDouble(MountId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MountId = reader.ReadDouble();
	}
}

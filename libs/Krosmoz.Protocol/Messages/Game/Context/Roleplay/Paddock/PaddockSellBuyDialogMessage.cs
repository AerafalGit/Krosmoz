// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Paddock;

public sealed class PaddockSellBuyDialogMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6018;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PaddockSellBuyDialogMessage Empty =>
		new() { Bsell = false, OwnerId = 0, Price = 0 };

	public required bool Bsell { get; set; }

	public required uint OwnerId { get; set; }

	public required uint Price { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Bsell);
		writer.WriteVarUInt32(OwnerId);
		writer.WriteVarUInt32(Price);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Bsell = reader.ReadBoolean();
		OwnerId = reader.ReadVarUInt32();
		Price = reader.ReadVarUInt32();
	}
}

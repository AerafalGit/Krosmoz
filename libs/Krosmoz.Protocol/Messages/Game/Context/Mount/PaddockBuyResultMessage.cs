// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class PaddockBuyResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6516;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PaddockBuyResultMessage Empty =>
		new() { PaddockId = 0, Bought = false, RealPrice = 0 };

	public required int PaddockId { get; set; }

	public required bool Bought { get; set; }

	public required uint RealPrice { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(PaddockId);
		writer.WriteBoolean(Bought);
		writer.WriteVarUInt32(RealPrice);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PaddockId = reader.ReadInt32();
		Bought = reader.ReadBoolean();
		RealPrice = reader.ReadVarUInt32();
	}
}

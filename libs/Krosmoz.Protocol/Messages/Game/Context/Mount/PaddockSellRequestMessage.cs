// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class PaddockSellRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5953;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PaddockSellRequestMessage Empty =>
		new() { Price = 0 };

	public required uint Price { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Price);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Price = reader.ReadVarUInt32();
	}
}

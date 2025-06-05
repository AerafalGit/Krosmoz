// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public sealed class HouseBuyRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5738;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseBuyRequestMessage Empty =>
		new() { ProposedPrice = 0 };

	public required uint ProposedPrice { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ProposedPrice);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ProposedPrice = reader.ReadVarUInt32();
	}
}

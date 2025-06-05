// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public class HouseSellRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5697;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseSellRequestMessage Empty =>
		new() { Amount = 0 };

	public required uint Amount { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Amount);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Amount = reader.ReadVarUInt32();
	}
}

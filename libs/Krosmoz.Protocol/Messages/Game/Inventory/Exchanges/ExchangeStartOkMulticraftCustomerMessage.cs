// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartOkMulticraftCustomerMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5817;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeStartOkMulticraftCustomerMessage Empty =>
		new() { SkillId = 0, CrafterJobLevel = 0 };

	public required uint SkillId { get; set; }

	public required byte CrafterJobLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(SkillId);
		writer.WriteUInt8(CrafterJobLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SkillId = reader.ReadVarUInt32();
		CrafterJobLevel = reader.ReadUInt8();
	}
}

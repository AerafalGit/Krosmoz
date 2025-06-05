// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartOkMulticraftCrafterMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5818;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeStartOkMulticraftCrafterMessage Empty =>
		new() { SkillId = 0 };

	public required uint SkillId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(SkillId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SkillId = reader.ReadVarUInt32();
	}
}

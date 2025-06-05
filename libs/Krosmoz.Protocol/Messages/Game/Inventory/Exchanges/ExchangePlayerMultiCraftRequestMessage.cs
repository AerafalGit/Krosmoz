// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangePlayerMultiCraftRequestMessage : ExchangeRequestMessage
{
	public new const uint StaticProtocolId = 5784;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangePlayerMultiCraftRequestMessage Empty =>
		new() { ExchangeType = 0, Target = 0, SkillId = 0 };

	public required uint Target { get; set; }

	public required uint SkillId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Target);
		writer.WriteVarUInt32(SkillId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Target = reader.ReadVarUInt32();
		SkillId = reader.ReadVarUInt32();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartOkCraftWithInformationMessage : ExchangeStartOkCraftMessage
{
	public new const uint StaticProtocolId = 5941;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeStartOkCraftWithInformationMessage Empty =>
		new() { NbCase = 0, SkillId = 0 };

	public required sbyte NbCase { get; set; }

	public required int SkillId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteSByte(NbCase);
		writer.WriteInt(SkillId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		NbCase = reader.ReadSByte();
		SkillId = reader.ReadInt();
	}
}

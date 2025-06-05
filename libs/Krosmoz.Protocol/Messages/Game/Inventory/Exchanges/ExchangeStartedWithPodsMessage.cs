// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartedWithPodsMessage : ExchangeStartedMessage
{
	public new const uint StaticProtocolId = 6129;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeStartedWithPodsMessage Empty =>
		new() { ExchangeType = 0, FirstCharacterId = 0, FirstCharacterCurrentWeight = 0, FirstCharacterMaxWeight = 0, SecondCharacterId = 0, SecondCharacterCurrentWeight = 0, SecondCharacterMaxWeight = 0 };

	public required int FirstCharacterId { get; set; }

	public required uint FirstCharacterCurrentWeight { get; set; }

	public required uint FirstCharacterMaxWeight { get; set; }

	public required int SecondCharacterId { get; set; }

	public required uint SecondCharacterCurrentWeight { get; set; }

	public required uint SecondCharacterMaxWeight { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(FirstCharacterId);
		writer.WriteVarUInt32(FirstCharacterCurrentWeight);
		writer.WriteVarUInt32(FirstCharacterMaxWeight);
		writer.WriteInt32(SecondCharacterId);
		writer.WriteVarUInt32(SecondCharacterCurrentWeight);
		writer.WriteVarUInt32(SecondCharacterMaxWeight);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		FirstCharacterId = reader.ReadInt32();
		FirstCharacterCurrentWeight = reader.ReadVarUInt32();
		FirstCharacterMaxWeight = reader.ReadVarUInt32();
		SecondCharacterId = reader.ReadInt32();
		SecondCharacterCurrentWeight = reader.ReadVarUInt32();
		SecondCharacterMaxWeight = reader.ReadVarUInt32();
	}
}

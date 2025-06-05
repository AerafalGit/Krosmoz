// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeTypesExchangerDescriptionForUserMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5765;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeTypesExchangerDescriptionForUserMessage Empty =>
		new() { TypeDescription = [] };

	public required IEnumerable<uint> TypeDescription { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var typeDescriptionBefore = writer.Position;
		var typeDescriptionCount = 0;
		writer.WriteInt16(0);
		foreach (var item in TypeDescription)
		{
			writer.WriteVarUInt32(item);
			typeDescriptionCount++;
		}
		var typeDescriptionAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, typeDescriptionBefore);
		writer.WriteInt16((short)typeDescriptionCount);
		writer.Seek(SeekOrigin.Begin, typeDescriptionAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var typeDescriptionCount = reader.ReadInt16();
		var typeDescription = new uint[typeDescriptionCount];
		for (var i = 0; i < typeDescriptionCount; i++)
		{
			typeDescription[i] = reader.ReadVarUInt32();
		}
		TypeDescription = typeDescription;
	}
}

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

	public required IEnumerable<int> TypeDescription { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var typeDescriptionBefore = writer.Position;
		var typeDescriptionCount = 0;
		writer.WriteShort(0);
		foreach (var item in TypeDescription)
		{
			writer.WriteInt(item);
			typeDescriptionCount++;
		}
		var typeDescriptionAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, typeDescriptionBefore);
		writer.WriteShort((short)typeDescriptionCount);
		writer.Seek(SeekOrigin.Begin, typeDescriptionAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var typeDescriptionCount = reader.ReadShort();
		var typeDescription = new int[typeDescriptionCount];
		for (var i = 0; i < typeDescriptionCount; i++)
		{
			typeDescription[i] = reader.ReadInt();
		}
		TypeDescription = typeDescription;
	}
}

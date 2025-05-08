// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeObjectTransfertListFromInvMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6183;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeObjectTransfertListFromInvMessage Empty =>
		new() { Ids = [] };

	public required IEnumerable<int> Ids { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var idsBefore = writer.Position;
		var idsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Ids)
		{
			writer.WriteInt(item);
			idsCount++;
		}
		var idsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, idsBefore);
		writer.WriteShort((short)idsCount);
		writer.Seek(SeekOrigin.Begin, idsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var idsCount = reader.ReadShort();
		var ids = new int[idsCount];
		for (var i = 0; i < idsCount; i++)
		{
			ids[i] = reader.ReadInt();
		}
		Ids = ids;
	}
}

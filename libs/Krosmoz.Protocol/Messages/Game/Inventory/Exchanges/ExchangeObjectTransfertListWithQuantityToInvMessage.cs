// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeObjectTransfertListWithQuantityToInvMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6470;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeObjectTransfertListWithQuantityToInvMessage Empty =>
		new() { Ids = [], Qtys = [] };

	public required IEnumerable<uint> Ids { get; set; }

	public required IEnumerable<uint> Qtys { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var idsBefore = writer.Position;
		var idsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Ids)
		{
			writer.WriteVarUInt32(item);
			idsCount++;
		}
		var idsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, idsBefore);
		writer.WriteInt16((short)idsCount);
		writer.Seek(SeekOrigin.Begin, idsAfter);
		var qtysBefore = writer.Position;
		var qtysCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Qtys)
		{
			writer.WriteVarUInt32(item);
			qtysCount++;
		}
		var qtysAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, qtysBefore);
		writer.WriteInt16((short)qtysCount);
		writer.Seek(SeekOrigin.Begin, qtysAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var idsCount = reader.ReadInt16();
		var ids = new uint[idsCount];
		for (var i = 0; i < idsCount; i++)
		{
			ids[i] = reader.ReadVarUInt32();
		}
		Ids = ids;
		var qtysCount = reader.ReadInt16();
		var qtys = new uint[qtysCount];
		for (var i = 0; i < qtysCount; i++)
		{
			qtys[i] = reader.ReadVarUInt32();
		}
		Qtys = qtys;
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeHandleMountsStableMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6562;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeHandleMountsStableMessage Empty =>
		new() { ActionType = 0, RidesId = [] };

	public required sbyte ActionType { get; set; }

	public required IEnumerable<uint> RidesId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(ActionType);
		var ridesIdBefore = writer.Position;
		var ridesIdCount = 0;
		writer.WriteInt16(0);
		foreach (var item in RidesId)
		{
			writer.WriteVarUInt32(item);
			ridesIdCount++;
		}
		var ridesIdAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, ridesIdBefore);
		writer.WriteInt16((short)ridesIdCount);
		writer.Seek(SeekOrigin.Begin, ridesIdAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ActionType = reader.ReadInt8();
		var ridesIdCount = reader.ReadInt16();
		var ridesId = new uint[ridesIdCount];
		for (var i = 0; i < ridesIdCount; i++)
		{
			ridesId[i] = reader.ReadVarUInt32();
		}
		RidesId = ridesId;
	}
}

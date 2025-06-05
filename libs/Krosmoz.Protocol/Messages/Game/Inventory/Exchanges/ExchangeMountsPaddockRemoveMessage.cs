// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeMountsPaddockRemoveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6559;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeMountsPaddockRemoveMessage Empty =>
		new() { MountsId = [] };

	public required IEnumerable<int> MountsId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var mountsIdBefore = writer.Position;
		var mountsIdCount = 0;
		writer.WriteInt16(0);
		foreach (var item in MountsId)
		{
			writer.WriteVarInt32(item);
			mountsIdCount++;
		}
		var mountsIdAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, mountsIdBefore);
		writer.WriteInt16((short)mountsIdCount);
		writer.Seek(SeekOrigin.Begin, mountsIdAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var mountsIdCount = reader.ReadInt16();
		var mountsId = new int[mountsIdCount];
		for (var i = 0; i < mountsIdCount; i++)
		{
			mountsId[i] = reader.ReadVarInt32();
		}
		MountsId = mountsId;
	}
}

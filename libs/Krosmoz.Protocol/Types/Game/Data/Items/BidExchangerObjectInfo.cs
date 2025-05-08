// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items.Effects;

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class BidExchangerObjectInfo : DofusType
{
	public new const ushort StaticProtocolId = 122;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static BidExchangerObjectInfo Empty =>
		new() { ObjectUID = 0, Effects = [], Prices = [] };

	public required int ObjectUID { get; set; }

	public required IEnumerable<ObjectEffect> Effects { get; set; }

	public required IEnumerable<int> Prices { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ObjectUID);
		var effectsBefore = writer.Position;
		var effectsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Effects)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			effectsCount++;
		}
		var effectsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, effectsBefore);
		writer.WriteShort((short)effectsCount);
		writer.Seek(SeekOrigin.Begin, effectsAfter);
		var pricesBefore = writer.Position;
		var pricesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Prices)
		{
			writer.WriteInt(item);
			pricesCount++;
		}
		var pricesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, pricesBefore);
		writer.WriteShort((short)pricesCount);
		writer.Seek(SeekOrigin.Begin, pricesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectUID = reader.ReadInt();
		var effectsCount = reader.ReadShort();
		var effects = new ObjectEffect[effectsCount];
		for (var i = 0; i < effectsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<ObjectEffect>(reader.ReadUShort());
			entry.Deserialize(reader);
			effects[i] = entry;
		}
		Effects = effects;
		var pricesCount = reader.ReadShort();
		var prices = new int[pricesCount];
		for (var i = 0; i < pricesCount; i++)
		{
			prices[i] = reader.ReadInt();
		}
		Prices = prices;
	}
}

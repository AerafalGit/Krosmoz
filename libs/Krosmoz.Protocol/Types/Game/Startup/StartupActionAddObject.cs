// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Types.Game.Startup;

public sealed class StartupActionAddObject : DofusType
{
	public new const ushort StaticProtocolId = 52;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static StartupActionAddObject Empty =>
		new() { Uid = 0, Title = string.Empty, Text = string.Empty, DescUrl = string.Empty, PictureUrl = string.Empty, Items = [] };

	public required int Uid { get; set; }

	public required string Title { get; set; }

	public required string Text { get; set; }

	public required string DescUrl { get; set; }

	public required string PictureUrl { get; set; }

	public required IEnumerable<ObjectItemInformationWithQuantity> Items { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(Uid);
		writer.WriteUtfLengthPrefixed16(Title);
		writer.WriteUtfLengthPrefixed16(Text);
		writer.WriteUtfLengthPrefixed16(DescUrl);
		writer.WriteUtfLengthPrefixed16(PictureUrl);
		var itemsBefore = writer.Position;
		var itemsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Items)
		{
			item.Serialize(writer);
			itemsCount++;
		}
		var itemsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, itemsBefore);
		writer.WriteShort((short)itemsCount);
		writer.Seek(SeekOrigin.Begin, itemsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Uid = reader.ReadInt();
		Title = reader.ReadUtfLengthPrefixed16();
		Text = reader.ReadUtfLengthPrefixed16();
		DescUrl = reader.ReadUtfLengthPrefixed16();
		PictureUrl = reader.ReadUtfLengthPrefixed16();
		var itemsCount = reader.ReadShort();
		var items = new ObjectItemInformationWithQuantity[itemsCount];
		for (var i = 0; i < itemsCount; i++)
		{
			var entry = ObjectItemInformationWithQuantity.Empty;
			entry.Deserialize(reader);
			items[i] = entry;
		}
		Items = items;
	}
}

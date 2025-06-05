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
		writer.WriteInt32(Uid);
		writer.WriteUtfPrefixedLength16(Title);
		writer.WriteUtfPrefixedLength16(Text);
		writer.WriteUtfPrefixedLength16(DescUrl);
		writer.WriteUtfPrefixedLength16(PictureUrl);
		var itemsBefore = writer.Position;
		var itemsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Items)
		{
			item.Serialize(writer);
			itemsCount++;
		}
		var itemsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, itemsBefore);
		writer.WriteInt16((short)itemsCount);
		writer.Seek(SeekOrigin.Begin, itemsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Uid = reader.ReadInt32();
		Title = reader.ReadUtfPrefixedLength16();
		Text = reader.ReadUtfPrefixedLength16();
		DescUrl = reader.ReadUtfPrefixedLength16();
		PictureUrl = reader.ReadUtfPrefixedLength16();
		var itemsCount = reader.ReadInt16();
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

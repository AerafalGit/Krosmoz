// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Paddock;

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class GameDataPaddockObjectListAddMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5992;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameDataPaddockObjectListAddMessage Empty =>
		new() { PaddockItemDescription = [] };

	public required IEnumerable<PaddockItem> PaddockItemDescription { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var paddockItemDescriptionBefore = writer.Position;
		var paddockItemDescriptionCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PaddockItemDescription)
		{
			item.Serialize(writer);
			paddockItemDescriptionCount++;
		}
		var paddockItemDescriptionAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, paddockItemDescriptionBefore);
		writer.WriteInt16((short)paddockItemDescriptionCount);
		writer.Seek(SeekOrigin.Begin, paddockItemDescriptionAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var paddockItemDescriptionCount = reader.ReadInt16();
		var paddockItemDescription = new PaddockItem[paddockItemDescriptionCount];
		for (var i = 0; i < paddockItemDescriptionCount; i++)
		{
			var entry = PaddockItem.Empty;
			entry.Deserialize(reader);
			paddockItemDescription[i] = entry;
		}
		PaddockItemDescription = paddockItemDescription;
	}
}

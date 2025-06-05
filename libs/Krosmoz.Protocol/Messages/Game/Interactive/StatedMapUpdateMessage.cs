// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Interactive;

namespace Krosmoz.Protocol.Messages.Game.Interactive;

public sealed class StatedMapUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5716;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static StatedMapUpdateMessage Empty =>
		new() { StatedElements = [] };

	public required IEnumerable<StatedElement> StatedElements { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var statedElementsBefore = writer.Position;
		var statedElementsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in StatedElements)
		{
			item.Serialize(writer);
			statedElementsCount++;
		}
		var statedElementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, statedElementsBefore);
		writer.WriteInt16((short)statedElementsCount);
		writer.Seek(SeekOrigin.Begin, statedElementsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var statedElementsCount = reader.ReadInt16();
		var statedElements = new StatedElement[statedElementsCount];
		for (var i = 0; i < statedElementsCount; i++)
		{
			var entry = StatedElement.Empty;
			entry.Deserialize(reader);
			statedElements[i] = entry;
		}
		StatedElements = statedElements;
	}
}

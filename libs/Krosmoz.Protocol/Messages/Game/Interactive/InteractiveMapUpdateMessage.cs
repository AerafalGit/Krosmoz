// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Interactive;

namespace Krosmoz.Protocol.Messages.Game.Interactive;

public sealed class InteractiveMapUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5002;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InteractiveMapUpdateMessage Empty =>
		new() { InteractiveElements = [] };

	public required IEnumerable<InteractiveElement> InteractiveElements { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var interactiveElementsBefore = writer.Position;
		var interactiveElementsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in InteractiveElements)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			interactiveElementsCount++;
		}
		var interactiveElementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, interactiveElementsBefore);
		writer.WriteInt16((short)interactiveElementsCount);
		writer.Seek(SeekOrigin.Begin, interactiveElementsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var interactiveElementsCount = reader.ReadInt16();
		var interactiveElements = new InteractiveElement[interactiveElementsCount];
		for (var i = 0; i < interactiveElementsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<InteractiveElement>(reader.ReadUInt16());
			entry.Deserialize(reader);
			interactiveElements[i] = entry;
		}
		InteractiveElements = interactiveElements;
	}
}

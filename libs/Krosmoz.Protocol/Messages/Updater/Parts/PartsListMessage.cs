// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Updater;

namespace Krosmoz.Protocol.Messages.Updater.Parts;

public sealed class PartsListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1502;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PartsListMessage Empty =>
		new() { Parts = [] };

	public required IEnumerable<ContentPart> Parts { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var partsBefore = writer.Position;
		var partsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Parts)
		{
			item.Serialize(writer);
			partsCount++;
		}
		var partsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, partsBefore);
		writer.WriteInt16((short)partsCount);
		writer.Seek(SeekOrigin.Begin, partsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var partsCount = reader.ReadInt16();
		var parts = new ContentPart[partsCount];
		for (var i = 0; i < partsCount; i++)
		{
			var entry = ContentPart.Empty;
			entry.Deserialize(reader);
			parts[i] = entry;
		}
		Parts = parts;
	}
}

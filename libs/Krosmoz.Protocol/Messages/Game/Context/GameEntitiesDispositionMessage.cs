// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameEntitiesDispositionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5696;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameEntitiesDispositionMessage Empty =>
		new() { Dispositions = [] };

	public required IEnumerable<IdentifiedEntityDispositionInformations> Dispositions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var dispositionsBefore = writer.Position;
		var dispositionsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Dispositions)
		{
			item.Serialize(writer);
			dispositionsCount++;
		}
		var dispositionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, dispositionsBefore);
		writer.WriteInt16((short)dispositionsCount);
		writer.Seek(SeekOrigin.Begin, dispositionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var dispositionsCount = reader.ReadInt16();
		var dispositions = new IdentifiedEntityDispositionInformations[dispositionsCount];
		for (var i = 0; i < dispositionsCount; i++)
		{
			var entry = IdentifiedEntityDispositionInformations.Empty;
			entry.Deserialize(reader);
			dispositions[i] = entry;
		}
		Dispositions = dispositions;
	}
}

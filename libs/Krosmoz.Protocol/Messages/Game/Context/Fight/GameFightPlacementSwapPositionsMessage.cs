// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightPlacementSwapPositionsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6544;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightPlacementSwapPositionsMessage Empty =>
		new() { Dispositions = [] };

	public required IEnumerable<IdentifiedEntityDispositionInformations> Dispositions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var dispositionsBefore = writer.Position;
		var dispositionsCount = 0;
		foreach (var item in Dispositions)
		{
			item.Serialize(writer);
			dispositionsCount++;
		}
		var dispositionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, dispositionsBefore);
		writer.Seek(SeekOrigin.Begin, dispositionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var dispositions = new IdentifiedEntityDispositionInformations[2];
		for (var i = 0; i < 2; i++)
		{
			var entry = IdentifiedEntityDispositionInformations.Empty;
			entry.Deserialize(reader);
			dispositions[i] = entry;
		}
		Dispositions = dispositions;
	}
}

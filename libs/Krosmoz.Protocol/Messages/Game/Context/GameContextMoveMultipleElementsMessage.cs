// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameContextMoveMultipleElementsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 254;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameContextMoveMultipleElementsMessage Empty =>
		new() { Movements = [] };

	public required IEnumerable<EntityMovementInformations> Movements { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var movementsBefore = writer.Position;
		var movementsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Movements)
		{
			item.Serialize(writer);
			movementsCount++;
		}
		var movementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, movementsBefore);
		writer.WriteInt16((short)movementsCount);
		writer.Seek(SeekOrigin.Begin, movementsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var movementsCount = reader.ReadInt16();
		var movements = new EntityMovementInformations[movementsCount];
		for (var i = 0; i < movementsCount; i++)
		{
			var entry = EntityMovementInformations.Empty;
			entry.Deserialize(reader);
			movements[i] = entry;
		}
		Movements = movements;
	}
}

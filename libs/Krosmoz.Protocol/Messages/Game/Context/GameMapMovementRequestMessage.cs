// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public class GameMapMovementRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 950;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameMapMovementRequestMessage Empty =>
		new() { KeyMovements = [], MapId = 0 };

	public required IEnumerable<short> KeyMovements { get; set; }

	public required int MapId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var keyMovementsBefore = writer.Position;
		var keyMovementsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in KeyMovements)
		{
			writer.WriteInt16(item);
			keyMovementsCount++;
		}
		var keyMovementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, keyMovementsBefore);
		writer.WriteInt16((short)keyMovementsCount);
		writer.Seek(SeekOrigin.Begin, keyMovementsAfter);
		writer.WriteInt32(MapId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var keyMovementsCount = reader.ReadInt16();
		var keyMovements = new short[keyMovementsCount];
		for (var i = 0; i < keyMovementsCount; i++)
		{
			keyMovements[i] = reader.ReadInt16();
		}
		KeyMovements = keyMovements;
		MapId = reader.ReadInt32();
	}
}

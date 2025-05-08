// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameMapMovementMessage : DofusMessage
{
	public new const uint StaticProtocolId = 951;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameMapMovementMessage Empty =>
		new() { KeyMovements = [], ActorId = 0 };

	public required IEnumerable<short> KeyMovements { get; set; }

	public required int ActorId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var keyMovementsBefore = writer.Position;
		var keyMovementsCount = 0;
		writer.WriteShort(0);
		foreach (var item in KeyMovements)
		{
			writer.WriteShort(item);
			keyMovementsCount++;
		}
		var keyMovementsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, keyMovementsBefore);
		writer.WriteShort((short)keyMovementsCount);
		writer.Seek(SeekOrigin.Begin, keyMovementsAfter);
		writer.WriteInt(ActorId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var keyMovementsCount = reader.ReadShort();
		var keyMovements = new short[keyMovementsCount];
		for (var i = 0; i < keyMovementsCount; i++)
		{
			keyMovements[i] = reader.ReadShort();
		}
		KeyMovements = keyMovements;
		ActorId = reader.ReadInt();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightLoot : DofusType
{
	public new const ushort StaticProtocolId = 41;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static FightLoot Empty =>
		new() { Objects = [], Kamas = 0 };

	public required IEnumerable<short> Objects { get; set; }

	public required int Kamas { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var objectsBefore = writer.Position;
		var objectsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Objects)
		{
			writer.WriteShort(item);
			objectsCount++;
		}
		var objectsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, objectsBefore);
		writer.WriteShort((short)objectsCount);
		writer.Seek(SeekOrigin.Begin, objectsAfter);
		writer.WriteInt(Kamas);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var objectsCount = reader.ReadShort();
		var objects = new short[objectsCount];
		for (var i = 0; i < objectsCount; i++)
		{
			objects[i] = reader.ReadShort();
		}
		Objects = objects;
		Kamas = reader.ReadInt();
	}
}

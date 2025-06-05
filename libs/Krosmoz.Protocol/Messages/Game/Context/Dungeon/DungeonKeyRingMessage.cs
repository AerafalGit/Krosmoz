// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Dungeon;

public sealed class DungeonKeyRingMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6299;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DungeonKeyRingMessage Empty =>
		new() { Availables = [], Unavailables = [] };

	public required IEnumerable<ushort> Availables { get; set; }

	public required IEnumerable<ushort> Unavailables { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var availablesBefore = writer.Position;
		var availablesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Availables)
		{
			writer.WriteVarUInt16(item);
			availablesCount++;
		}
		var availablesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, availablesBefore);
		writer.WriteInt16((short)availablesCount);
		writer.Seek(SeekOrigin.Begin, availablesAfter);
		var unavailablesBefore = writer.Position;
		var unavailablesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Unavailables)
		{
			writer.WriteVarUInt16(item);
			unavailablesCount++;
		}
		var unavailablesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, unavailablesBefore);
		writer.WriteInt16((short)unavailablesCount);
		writer.Seek(SeekOrigin.Begin, unavailablesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var availablesCount = reader.ReadInt16();
		var availables = new ushort[availablesCount];
		for (var i = 0; i < availablesCount; i++)
		{
			availables[i] = reader.ReadVarUInt16();
		}
		Availables = availables;
		var unavailablesCount = reader.ReadInt16();
		var unavailables = new ushort[unavailablesCount];
		for (var i = 0; i < unavailablesCount; i++)
		{
			unavailables[i] = reader.ReadVarUInt16();
		}
		Unavailables = unavailables;
	}
}

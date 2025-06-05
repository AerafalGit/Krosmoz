// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Notification;

public sealed class NotificationListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6087;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NotificationListMessage Empty =>
		new() { Flags = [] };

	public required IEnumerable<int> Flags { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flagsBefore = writer.Position;
		var flagsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Flags)
		{
			writer.WriteVarInt32(item);
			flagsCount++;
		}
		var flagsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, flagsBefore);
		writer.WriteInt16((short)flagsCount);
		writer.Seek(SeekOrigin.Begin, flagsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flagsCount = reader.ReadInt16();
		var flags = new int[flagsCount];
		for (var i = 0; i < flagsCount; i++)
		{
			flags[i] = reader.ReadVarInt32();
		}
		Flags = flags;
	}
}

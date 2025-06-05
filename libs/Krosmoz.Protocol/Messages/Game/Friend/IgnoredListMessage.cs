// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Friend;

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class IgnoredListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5674;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IgnoredListMessage Empty =>
		new() { IgnoredList = [] };

	public required IEnumerable<IgnoredInformations> IgnoredList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var ignoredListBefore = writer.Position;
		var ignoredListCount = 0;
		writer.WriteInt16(0);
		foreach (var item in IgnoredList)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			ignoredListCount++;
		}
		var ignoredListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, ignoredListBefore);
		writer.WriteInt16((short)ignoredListCount);
		writer.Seek(SeekOrigin.Begin, ignoredListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var ignoredListCount = reader.ReadInt16();
		var ignoredList = new IgnoredInformations[ignoredListCount];
		for (var i = 0; i < ignoredListCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<IgnoredInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			ignoredList[i] = entry;
		}
		IgnoredList = ignoredList;
	}
}

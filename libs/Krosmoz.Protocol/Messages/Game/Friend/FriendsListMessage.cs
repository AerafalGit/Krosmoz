// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Friend;

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class FriendsListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 4002;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static FriendsListMessage Empty =>
		new() { FriendsList = [] };

	public required IEnumerable<FriendInformations> FriendsList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var friendsListBefore = writer.Position;
		var friendsListCount = 0;
		writer.WriteShort(0);
		foreach (var item in FriendsList)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			friendsListCount++;
		}
		var friendsListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, friendsListBefore);
		writer.WriteShort((short)friendsListCount);
		writer.Seek(SeekOrigin.Begin, friendsListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var friendsListCount = reader.ReadShort();
		var friendsList = new FriendInformations[friendsListCount];
		for (var i = 0; i < friendsListCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<FriendInformations>(reader.ReadUShort());
			entry.Deserialize(reader);
			friendsList[i] = entry;
		}
		FriendsList = friendsList;
	}
}

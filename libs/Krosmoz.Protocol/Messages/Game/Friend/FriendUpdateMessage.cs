// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Friend;

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class FriendUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5924;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static FriendUpdateMessage Empty =>
		new() { FriendUpdated = FriendInformations.Empty };

	public required FriendInformations FriendUpdated { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(FriendUpdated.ProtocolId);
		FriendUpdated.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FriendUpdated = Types.TypeFactory.CreateType<FriendInformations>(reader.ReadUInt16());
		FriendUpdated.Deserialize(reader);
	}
}

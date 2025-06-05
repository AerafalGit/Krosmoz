// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Friend;

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class FriendAddedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5599;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static FriendAddedMessage Empty =>
		new() { FriendAdded = FriendInformations.Empty };

	public required FriendInformations FriendAdded { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(FriendAdded.ProtocolId);
		FriendAdded.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FriendAdded = Types.TypeFactory.CreateType<FriendInformations>(reader.ReadUInt16());
		FriendAdded.Deserialize(reader);
	}
}

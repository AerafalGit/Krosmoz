// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class FriendAddFailureMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5600;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static FriendAddFailureMessage Empty =>
		new() { Reason = 0 };

	public required sbyte Reason { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Reason);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Reason = reader.ReadInt8();
	}
}

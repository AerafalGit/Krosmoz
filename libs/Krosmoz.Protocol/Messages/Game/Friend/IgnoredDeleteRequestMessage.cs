// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class IgnoredDeleteRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5680;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IgnoredDeleteRequestMessage Empty =>
		new() { AccountId = 0, Session = false };

	public required int AccountId { get; set; }

	public required bool Session { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(AccountId);
		writer.WriteBoolean(Session);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AccountId = reader.ReadInt32();
		Session = reader.ReadBoolean();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class FriendDeleteResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5601;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static FriendDeleteResultMessage Empty =>
		new() { Success = false, Name = string.Empty };

	public required bool Success { get; set; }

	public required string Name { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Success);
		writer.WriteUtfPrefixedLength16(Name);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Success = reader.ReadBoolean();
		Name = reader.ReadUtfPrefixedLength16();
	}
}

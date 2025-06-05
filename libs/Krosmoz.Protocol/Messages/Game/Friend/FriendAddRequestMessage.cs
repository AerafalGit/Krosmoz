// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class FriendAddRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 4004;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static FriendAddRequestMessage Empty =>
		new() { Name = string.Empty };

	public required string Name { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Name);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Name = reader.ReadUtfPrefixedLength16();
	}
}

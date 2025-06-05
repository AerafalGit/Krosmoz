// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class IgnoredAddRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5673;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IgnoredAddRequestMessage Empty =>
		new() { Name = string.Empty, Session = false };

	public required string Name { get; set; }

	public required bool Session { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Name);
		writer.WriteBoolean(Session);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Name = reader.ReadUtfPrefixedLength16();
		Session = reader.ReadBoolean();
	}
}

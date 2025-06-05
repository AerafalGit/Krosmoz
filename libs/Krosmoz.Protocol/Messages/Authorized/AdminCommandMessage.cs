// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Authorized;

public class AdminCommandMessage : DofusMessage
{
	public new const uint StaticProtocolId = 76;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AdminCommandMessage Empty =>
		new() { Content = string.Empty };

	public required string Content { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Content);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Content = reader.ReadUtfPrefixedLength16();
	}
}

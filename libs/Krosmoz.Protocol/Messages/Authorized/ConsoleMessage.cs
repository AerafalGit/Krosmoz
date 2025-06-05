// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Authorized;

public sealed class ConsoleMessage : DofusMessage
{
	public new const uint StaticProtocolId = 75;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ConsoleMessage Empty =>
		new() { Type = 0, Content = string.Empty };

	public required sbyte Type { get; set; }

	public required string Content { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Type);
		writer.WriteUtfPrefixedLength16(Content);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Type = reader.ReadInt8();
		Content = reader.ReadUtfPrefixedLength16();
	}
}

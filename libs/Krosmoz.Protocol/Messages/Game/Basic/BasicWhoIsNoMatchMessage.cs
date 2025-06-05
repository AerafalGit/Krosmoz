// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicWhoIsNoMatchMessage : DofusMessage
{
	public new const uint StaticProtocolId = 179;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicWhoIsNoMatchMessage Empty =>
		new() { Search = string.Empty };

	public required string Search { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Search);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Search = reader.ReadUtfPrefixedLength16();
	}
}

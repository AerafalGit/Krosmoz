// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicWhoIsRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 181;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicWhoIsRequestMessage Empty =>
		new() { Verbose = false, Search = string.Empty };

	public required bool Verbose { get; set; }

	public required string Search { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Verbose);
		writer.WriteUtfPrefixedLength16(Search);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Verbose = reader.ReadBoolean();
		Search = reader.ReadUtfPrefixedLength16();
	}
}

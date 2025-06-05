// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Web.Krosmaster;

public sealed class KrosmasterAuthTokenMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6351;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static KrosmasterAuthTokenMessage Empty =>
		new() { Token = string.Empty };

	public required string Token { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Token);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Token = reader.ReadUtfPrefixedLength16();
	}
}

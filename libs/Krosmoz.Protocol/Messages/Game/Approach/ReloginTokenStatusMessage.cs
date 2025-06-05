// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Approach;

public sealed class ReloginTokenStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6539;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ReloginTokenStatusMessage Empty =>
		new() { ValidToken = false, Token = string.Empty };

	public required bool ValidToken { get; set; }

	public required string Token { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(ValidToken);
		writer.WriteUtfPrefixedLength16(Token);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ValidToken = reader.ReadBoolean();
		Token = reader.ReadUtfPrefixedLength16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Lockable;

public class LockableChangeCodeMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5666;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static LockableChangeCodeMessage Empty =>
		new() { Code = string.Empty };

	public required string Code { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Code);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Code = reader.ReadUtfPrefixedLength16();
	}
}

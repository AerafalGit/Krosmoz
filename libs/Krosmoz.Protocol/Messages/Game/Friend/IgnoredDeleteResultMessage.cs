// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class IgnoredDeleteResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5677;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IgnoredDeleteResultMessage Empty =>
		new() { Success = false, Session = false, Name = string.Empty };

	public required bool Success { get; set; }

	public required bool Session { get; set; }

	public required string Name { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, Success);
		flag = BooleanByteWrapper.SetFlag(flag, 1, Session);
		writer.WriteUInt8(flag);
		writer.WriteUtfPrefixedLength16(Name);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		Success = BooleanByteWrapper.GetFlag(flag, 0);
		Session = BooleanByteWrapper.GetFlag(flag, 1);
		Name = reader.ReadUtfPrefixedLength16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Debug;

public sealed class DebugInClientMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6028;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DebugInClientMessage Empty =>
		new() { Level = 0, Message = string.Empty };

	public required sbyte Level { get; set; }

	public required string Message { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Level);
		writer.WriteUtfPrefixedLength16(Message);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Level = reader.ReadInt8();
		Message = reader.ReadUtfPrefixedLength16();
	}
}

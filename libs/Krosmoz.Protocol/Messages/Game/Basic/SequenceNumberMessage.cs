// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class SequenceNumberMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6317;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SequenceNumberMessage Empty =>
		new() { Number = 0 };

	public required ushort Number { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Number);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Number = reader.ReadUInt16();
	}
}

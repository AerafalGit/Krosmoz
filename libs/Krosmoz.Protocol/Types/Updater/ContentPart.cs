// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Updater;

public sealed class ContentPart : DofusType
{
	public new const ushort StaticProtocolId = 350;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static ContentPart Empty =>
		new() { Id = string.Empty, State = 0 };

	public required string Id { get; set; }

	public required sbyte State { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfLengthPrefixed16(Id);
		writer.WriteSByte(State);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadUtfLengthPrefixed16();
		State = reader.ReadSByte();
	}
}

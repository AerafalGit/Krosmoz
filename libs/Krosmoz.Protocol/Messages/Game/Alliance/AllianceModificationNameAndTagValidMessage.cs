// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceModificationNameAndTagValidMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6449;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceModificationNameAndTagValidMessage Empty =>
		new() { AllianceName = string.Empty, AllianceTag = string.Empty };

	public required string AllianceName { get; set; }

	public required string AllianceTag { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfLengthPrefixed16(AllianceName);
		writer.WriteUtfLengthPrefixed16(AllianceTag);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AllianceName = reader.ReadUtfLengthPrefixed16();
		AllianceTag = reader.ReadUtfLengthPrefixed16();
	}
}

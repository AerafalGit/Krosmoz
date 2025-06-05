// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceCreationValidMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6393;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceCreationValidMessage Empty =>
		new() { AllianceName = string.Empty, AllianceTag = string.Empty, AllianceEmblem = GuildEmblem.Empty };

	public required string AllianceName { get; set; }

	public required string AllianceTag { get; set; }

	public required GuildEmblem AllianceEmblem { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(AllianceName);
		writer.WriteUtfPrefixedLength16(AllianceTag);
		AllianceEmblem.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AllianceName = reader.ReadUtfPrefixedLength16();
		AllianceTag = reader.ReadUtfPrefixedLength16();
		AllianceEmblem = GuildEmblem.Empty;
		AllianceEmblem.Deserialize(reader);
	}
}

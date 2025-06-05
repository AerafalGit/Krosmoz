// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public class BasicNamedAllianceInformations : BasicAllianceInformations
{
	public new const ushort StaticProtocolId = 418;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new BasicNamedAllianceInformations Empty =>
		new() { AllianceTag = string.Empty, AllianceId = 0, AllianceName = string.Empty };

	public required string AllianceName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(AllianceName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AllianceName = reader.ReadUtfPrefixedLength16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Social;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public class BasicAllianceInformations : AbstractSocialGroupInfos
{
	public new const ushort StaticProtocolId = 419;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new BasicAllianceInformations Empty =>
		new() { AllianceId = 0, AllianceTag = string.Empty };

	public required uint AllianceId { get; set; }

	public required string AllianceTag { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(AllianceId);
		writer.WriteUtfPrefixedLength16(AllianceTag);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AllianceId = reader.ReadVarUInt32();
		AllianceTag = reader.ReadUtfPrefixedLength16();
	}
}

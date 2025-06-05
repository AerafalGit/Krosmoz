// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Types.Game.Social;

public sealed class AllianceFactSheetInformations : AllianceInformations
{
	public new const ushort StaticProtocolId = 421;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new AllianceFactSheetInformations Empty =>
		new() { AllianceTag = string.Empty, AllianceId = 0, AllianceName = string.Empty, AllianceEmblem = GuildEmblem.Empty, CreationDate = 0 };

	public required int CreationDate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(CreationDate);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CreationDate = reader.ReadInt32();
	}
}

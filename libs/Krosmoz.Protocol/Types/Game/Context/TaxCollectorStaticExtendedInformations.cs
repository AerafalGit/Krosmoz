// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Types.Game.Context;

public sealed class TaxCollectorStaticExtendedInformations : TaxCollectorStaticInformations
{
	public new const ushort StaticProtocolId = 440;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new TaxCollectorStaticExtendedInformations Empty =>
		new() { GuildIdentity = GuildInformations.Empty, LastNameId = 0, FirstNameId = 0, AllianceIdentity = AllianceInformations.Empty };

	public required AllianceInformations AllianceIdentity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		AllianceIdentity.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AllianceIdentity = AllianceInformations.Empty;
		AllianceIdentity.Deserialize(reader);
	}
}

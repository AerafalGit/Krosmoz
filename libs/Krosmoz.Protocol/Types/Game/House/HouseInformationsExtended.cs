// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Types.Game.House;

public sealed class HouseInformationsExtended : HouseInformations
{
	public new const ushort StaticProtocolId = 112;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new HouseInformationsExtended Empty =>
		new() { ModelId = 0, OwnerName = string.Empty, DoorsOnMap = [], HouseId = 0, IsSaleLocked = false, IsOnSale = false, GuildInfo = GuildInformations.Empty };

	public required GuildInformations GuildInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		GuildInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		GuildInfo = GuildInformations.Empty;
		GuildInfo.Deserialize(reader);
	}
}

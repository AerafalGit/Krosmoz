// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.House;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildHouseUpdateInformationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6181;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildHouseUpdateInformationMessage Empty =>
		new() { HousesInformations = HouseInformationsForGuild.Empty };

	public required HouseInformationsForGuild HousesInformations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		HousesInformations.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HousesInformations = HouseInformationsForGuild.Empty;
		HousesInformations.Deserialize(reader);
	}
}

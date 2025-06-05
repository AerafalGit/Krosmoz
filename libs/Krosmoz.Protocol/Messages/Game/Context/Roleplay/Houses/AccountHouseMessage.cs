// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.House;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public sealed class AccountHouseMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6315;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AccountHouseMessage Empty =>
		new() { Houses = [] };

	public required IEnumerable<AccountHouseInformations> Houses { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var housesBefore = writer.Position;
		var housesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Houses)
		{
			item.Serialize(writer);
			housesCount++;
		}
		var housesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, housesBefore);
		writer.WriteInt16((short)housesCount);
		writer.Seek(SeekOrigin.Begin, housesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var housesCount = reader.ReadInt16();
		var houses = new AccountHouseInformations[housesCount];
		for (var i = 0; i < housesCount; i++)
		{
			var entry = AccountHouseInformations.Empty;
			entry.Deserialize(reader);
			houses[i] = entry;
		}
		Houses = houses;
	}
}

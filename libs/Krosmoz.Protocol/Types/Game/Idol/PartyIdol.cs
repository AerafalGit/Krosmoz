// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Idol;

public sealed class PartyIdol : Idol
{
	public new const ushort StaticProtocolId = 490;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PartyIdol Empty =>
		new() { DropBonusPercent = 0, XpBonusPercent = 0, Id = 0, OwnersIds = [] };

	public required IEnumerable<int> OwnersIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var ownersIdsBefore = writer.Position;
		var ownersIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in OwnersIds)
		{
			writer.WriteInt32(item);
			ownersIdsCount++;
		}
		var ownersIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, ownersIdsBefore);
		writer.WriteInt16((short)ownersIdsCount);
		writer.Seek(SeekOrigin.Begin, ownersIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var ownersIdsCount = reader.ReadInt16();
		var ownersIds = new int[ownersIdsCount];
		for (var i = 0; i < ownersIdsCount; i++)
		{
			ownersIds[i] = reader.ReadInt32();
		}
		OwnersIds = ownersIds;
	}
}

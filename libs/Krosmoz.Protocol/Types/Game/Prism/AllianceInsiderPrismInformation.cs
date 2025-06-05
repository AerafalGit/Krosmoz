// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Prism;

public sealed class AllianceInsiderPrismInformation : PrismInformation
{
	public new const ushort StaticProtocolId = 431;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new AllianceInsiderPrismInformation Empty =>
		new() { RewardTokenCount = 0, PlacementDate = 0, NextVulnerabilityDate = 0, State = 0, TypeId = 0, LastTimeSlotModificationDate = 0, LastTimeSlotModificationAuthorGuildId = 0, LastTimeSlotModificationAuthorId = 0, LastTimeSlotModificationAuthorName = string.Empty, ModulesItemIds = [] };

	public required int LastTimeSlotModificationDate { get; set; }

	public required uint LastTimeSlotModificationAuthorGuildId { get; set; }

	public required uint LastTimeSlotModificationAuthorId { get; set; }

	public required string LastTimeSlotModificationAuthorName { get; set; }

	public required IEnumerable<uint> ModulesItemIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(LastTimeSlotModificationDate);
		writer.WriteVarUInt32(LastTimeSlotModificationAuthorGuildId);
		writer.WriteVarUInt32(LastTimeSlotModificationAuthorId);
		writer.WriteUtfPrefixedLength16(LastTimeSlotModificationAuthorName);
		var modulesItemIdsBefore = writer.Position;
		var modulesItemIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ModulesItemIds)
		{
			writer.WriteVarUInt32(item);
			modulesItemIdsCount++;
		}
		var modulesItemIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, modulesItemIdsBefore);
		writer.WriteInt16((short)modulesItemIdsCount);
		writer.Seek(SeekOrigin.Begin, modulesItemIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		LastTimeSlotModificationDate = reader.ReadInt32();
		LastTimeSlotModificationAuthorGuildId = reader.ReadVarUInt32();
		LastTimeSlotModificationAuthorId = reader.ReadVarUInt32();
		LastTimeSlotModificationAuthorName = reader.ReadUtfPrefixedLength16();
		var modulesItemIdsCount = reader.ReadInt16();
		var modulesItemIds = new uint[modulesItemIdsCount];
		for (var i = 0; i < modulesItemIdsCount; i++)
		{
			modulesItemIds[i] = reader.ReadVarUInt32();
		}
		ModulesItemIds = modulesItemIds;
	}
}

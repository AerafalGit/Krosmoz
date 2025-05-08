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
		new() { PlacementDate = 0, NextVulnerabilityDate = 0, State = 0, TypeId = 0, LastTimeSlotModificationDate = 0, LastTimeSlotModificationAuthorGuildId = 0, LastTimeSlotModificationAuthorId = 0, LastTimeSlotModificationAuthorName = string.Empty, HasTeleporterModule = false };

	public required int LastTimeSlotModificationDate { get; set; }

	public required int LastTimeSlotModificationAuthorGuildId { get; set; }

	public required int LastTimeSlotModificationAuthorId { get; set; }

	public required string LastTimeSlotModificationAuthorName { get; set; }

	public required bool HasTeleporterModule { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(LastTimeSlotModificationDate);
		writer.WriteInt(LastTimeSlotModificationAuthorGuildId);
		writer.WriteInt(LastTimeSlotModificationAuthorId);
		writer.WriteUtfLengthPrefixed16(LastTimeSlotModificationAuthorName);
		writer.WriteBoolean(HasTeleporterModule);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		LastTimeSlotModificationDate = reader.ReadInt();
		LastTimeSlotModificationAuthorGuildId = reader.ReadInt();
		LastTimeSlotModificationAuthorId = reader.ReadInt();
		LastTimeSlotModificationAuthorName = reader.ReadUtfLengthPrefixed16();
		HasTeleporterModule = reader.ReadBoolean();
	}
}

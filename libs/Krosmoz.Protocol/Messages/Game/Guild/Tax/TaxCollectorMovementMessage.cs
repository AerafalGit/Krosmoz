// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild.Tax;

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class TaxCollectorMovementMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5633;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TaxCollectorMovementMessage Empty =>
		new() { HireOrFire = false, BasicInfos = TaxCollectorBasicInformations.Empty, PlayerId = 0, PlayerName = string.Empty };

	public required bool HireOrFire { get; set; }

	public required TaxCollectorBasicInformations BasicInfos { get; set; }

	public required uint PlayerId { get; set; }

	public required string PlayerName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(HireOrFire);
		BasicInfos.Serialize(writer);
		writer.WriteVarUInt32(PlayerId);
		writer.WriteUtfPrefixedLength16(PlayerName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HireOrFire = reader.ReadBoolean();
		BasicInfos = TaxCollectorBasicInformations.Empty;
		BasicInfos.Deserialize(reader);
		PlayerId = reader.ReadVarUInt32();
		PlayerName = reader.ReadUtfPrefixedLength16();
	}
}

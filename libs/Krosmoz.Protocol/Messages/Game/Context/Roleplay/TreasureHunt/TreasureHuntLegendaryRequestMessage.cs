// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntLegendaryRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6499;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TreasureHuntLegendaryRequestMessage Empty =>
		new() { LegendaryId = 0 };

	public required ushort LegendaryId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(LegendaryId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		LegendaryId = reader.ReadVarUInt16();
	}
}

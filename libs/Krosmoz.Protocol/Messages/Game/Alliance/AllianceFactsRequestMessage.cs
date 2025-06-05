// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceFactsRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6409;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceFactsRequestMessage Empty =>
		new() { AllianceId = 0 };

	public required uint AllianceId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(AllianceId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AllianceId = reader.ReadVarUInt32();
	}
}

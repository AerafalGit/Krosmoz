// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class GameRolePlayTaxCollectorFightRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5954;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayTaxCollectorFightRequestMessage Empty =>
		new() { TaxCollectorId = 0 };

	public required int TaxCollectorId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(TaxCollectorId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TaxCollectorId = reader.ReadInt32();
	}
}

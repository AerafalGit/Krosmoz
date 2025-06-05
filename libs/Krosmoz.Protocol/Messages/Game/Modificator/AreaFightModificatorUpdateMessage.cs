// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Modificator;

public sealed class AreaFightModificatorUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6493;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AreaFightModificatorUpdateMessage Empty =>
		new() { SpellPairId = 0 };

	public required int SpellPairId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(SpellPairId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpellPairId = reader.ReadInt32();
	}
}

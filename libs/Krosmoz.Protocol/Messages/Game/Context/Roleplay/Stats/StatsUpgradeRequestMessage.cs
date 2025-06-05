// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Stats;

public sealed class StatsUpgradeRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5610;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static StatsUpgradeRequestMessage Empty =>
		new() { UseAdditionnal = false, StatId = 0, BoostPoint = 0 };

	public required bool UseAdditionnal { get; set; }

	public required sbyte StatId { get; set; }

	public required ushort BoostPoint { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(UseAdditionnal);
		writer.WriteInt8(StatId);
		writer.WriteVarUInt16(BoostPoint);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		UseAdditionnal = reader.ReadBoolean();
		StatId = reader.ReadInt8();
		BoostPoint = reader.ReadVarUInt16();
	}
}

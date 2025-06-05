// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Fight;

public sealed class ProtectedEntityWaitingForHelpInfo : DofusType
{
	public new const ushort StaticProtocolId = 186;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static ProtectedEntityWaitingForHelpInfo Empty =>
		new() { TimeLeftBeforeFight = 0, WaitTimeForPlacement = 0, NbPositionForDefensors = 0 };

	public required int TimeLeftBeforeFight { get; set; }

	public required int WaitTimeForPlacement { get; set; }

	public required sbyte NbPositionForDefensors { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(TimeLeftBeforeFight);
		writer.WriteInt32(WaitTimeForPlacement);
		writer.WriteInt8(NbPositionForDefensors);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TimeLeftBeforeFight = reader.ReadInt32();
		WaitTimeForPlacement = reader.ReadInt32();
		NbPositionForDefensors = reader.ReadInt8();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Achievement;

public sealed class AchievementStartedObjective : AchievementObjective
{
	public new const ushort StaticProtocolId = 402;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new AchievementStartedObjective Empty =>
		new() { MaxValue = 0, Id = 0, Value = 0 };

	public required short Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(Value);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Value = reader.ReadInt16();
	}
}

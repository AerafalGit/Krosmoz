// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Achievement;

public class AchievementObjective : DofusType
{
	public new const ushort StaticProtocolId = 404;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AchievementObjective Empty =>
		new() { Id = 0, MaxValue = 0 };

	public required int Id { get; set; }

	public required short MaxValue { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(Id);
		writer.WriteShort(MaxValue);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadInt();
		MaxValue = reader.ReadShort();
	}
}

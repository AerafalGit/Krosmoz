// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Achievement;

public sealed class AchievementDetailsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6378;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AchievementDetailsMessage Empty =>
		new() { Achievement = Types.Game.Achievement.Achievement.Empty };

	public required Types.Game.Achievement.Achievement Achievement { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Achievement.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Achievement = Types.Game.Achievement.Achievement.Empty;
		Achievement.Deserialize(reader);
	}
}

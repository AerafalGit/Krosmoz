// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Achievement;

public class AchievementFinishedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6208;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AchievementFinishedMessage Empty =>
		new() { Id = 0, Finishedlevel = 0 };

	public required short Id { get; set; }

	public required short Finishedlevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(Id);
		writer.WriteShort(Finishedlevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadShort();
		Finishedlevel = reader.ReadShort();
	}
}

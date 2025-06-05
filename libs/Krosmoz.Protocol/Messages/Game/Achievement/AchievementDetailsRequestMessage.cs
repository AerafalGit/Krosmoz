// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Achievement;

public sealed class AchievementDetailsRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6380;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AchievementDetailsRequestMessage Empty =>
		new() { AchievementId = 0 };

	public required ushort AchievementId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(AchievementId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AchievementId = reader.ReadVarUInt16();
	}
}

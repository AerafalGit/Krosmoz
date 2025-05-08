// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight.Challenge;

public sealed class ChallengeInfoMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6022;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ChallengeInfoMessage Empty =>
		new() { ChallengeId = 0, TargetId = 0, XpBonus = 0, DropBonus = 0 };

	public required short ChallengeId { get; set; }

	public required int TargetId { get; set; }

	public required int XpBonus { get; set; }

	public required int DropBonus { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(ChallengeId);
		writer.WriteInt(TargetId);
		writer.WriteInt(XpBonus);
		writer.WriteInt(DropBonus);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ChallengeId = reader.ReadShort();
		TargetId = reader.ReadInt();
		XpBonus = reader.ReadInt();
		DropBonus = reader.ReadInt();
	}
}

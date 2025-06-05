// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight.Challenge;

public sealed class ChallengeTargetUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6123;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ChallengeTargetUpdateMessage Empty =>
		new() { ChallengeId = 0, TargetId = 0 };

	public required ushort ChallengeId { get; set; }

	public required int TargetId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ChallengeId);
		writer.WriteInt32(TargetId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ChallengeId = reader.ReadVarUInt16();
		TargetId = reader.ReadInt32();
	}
}

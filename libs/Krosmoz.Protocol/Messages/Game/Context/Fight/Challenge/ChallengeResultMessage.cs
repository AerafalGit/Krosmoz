// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight.Challenge;

public sealed class ChallengeResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6019;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ChallengeResultMessage Empty =>
		new() { ChallengeId = 0, Success = false };

	public required ushort ChallengeId { get; set; }

	public required bool Success { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ChallengeId);
		writer.WriteBoolean(Success);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ChallengeId = reader.ReadVarUInt16();
		Success = reader.ReadBoolean();
	}
}

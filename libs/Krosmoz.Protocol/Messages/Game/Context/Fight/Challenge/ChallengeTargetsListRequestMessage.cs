// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight.Challenge;

public sealed class ChallengeTargetsListRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5614;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ChallengeTargetsListRequestMessage Empty =>
		new() { ChallengeId = 0 };

	public required ushort ChallengeId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ChallengeId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ChallengeId = reader.ReadVarUInt16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class ChallengeFightJoinRefusedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5908;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ChallengeFightJoinRefusedMessage Empty =>
		new() { PlayerId = 0, Reason = 0 };

	public required uint PlayerId { get; set; }

	public required sbyte Reason { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(PlayerId);
		writer.WriteInt8(Reason);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PlayerId = reader.ReadVarUInt32();
		Reason = reader.ReadInt8();
	}
}

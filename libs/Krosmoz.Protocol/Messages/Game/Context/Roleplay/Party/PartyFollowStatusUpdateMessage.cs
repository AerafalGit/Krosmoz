// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyFollowStatusUpdateMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 5581;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyFollowStatusUpdateMessage Empty =>
		new() { PartyId = 0, Success = false, FollowedId = 0 };

	public required bool Success { get; set; }

	public required uint FollowedId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(Success);
		writer.WriteVarUInt32(FollowedId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Success = reader.ReadBoolean();
		FollowedId = reader.ReadVarUInt32();
	}
}

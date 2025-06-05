// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyInvitationDungeonRequestMessage : PartyInvitationRequestMessage
{
	public new const uint StaticProtocolId = 6245;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyInvitationDungeonRequestMessage Empty =>
		new() { Name = string.Empty, DungeonId = 0 };

	public required ushort DungeonId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(DungeonId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		DungeonId = reader.ReadVarUInt16();
	}
}

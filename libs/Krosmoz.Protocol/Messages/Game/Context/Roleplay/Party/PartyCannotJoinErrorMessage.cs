// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyCannotJoinErrorMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 5583;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyCannotJoinErrorMessage Empty =>
		new() { PartyId = 0, Reason = 0 };

	public required sbyte Reason { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Reason);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Reason = reader.ReadInt8();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public class PartyUpdateMessage : AbstractPartyEventMessage
{
	public new const uint StaticProtocolId = 5575;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyUpdateMessage Empty =>
		new() { PartyId = 0, MemberInformations = PartyMemberInformations.Empty };

	public required PartyMemberInformations MemberInformations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt16(MemberInformations.ProtocolId);
		MemberInformations.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MemberInformations = Types.TypeFactory.CreateType<PartyMemberInformations>(reader.ReadUInt16());
		MemberInformations.Deserialize(reader);
	}
}

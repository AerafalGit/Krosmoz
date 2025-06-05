// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyNameSetErrorMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 6501;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyNameSetErrorMessage Empty =>
		new() { PartyId = 0, Result = 0 };

	public required sbyte Result { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Result);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Result = reader.ReadInt8();
	}
}

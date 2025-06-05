// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party.Companion;

public sealed class PartyCompanionUpdateLightMessage : PartyUpdateLightMessage
{
	public new const uint StaticProtocolId = 6472;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyCompanionUpdateLightMessage Empty =>
		new() { PartyId = 0, RegenRate = 0, Prospecting = 0, MaxLifePoints = 0, LifePoints = 0, Id = 0, IndexId = 0 };

	public required sbyte IndexId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(IndexId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		IndexId = reader.ReadInt8();
	}
}

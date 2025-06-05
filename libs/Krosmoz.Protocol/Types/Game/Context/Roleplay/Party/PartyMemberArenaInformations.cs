// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Status;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

public sealed class PartyMemberArenaInformations : PartyMemberInformations
{
	public new const ushort StaticProtocolId = 391;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PartyMemberArenaInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty, Sex = false, Breed = 0, Companions = [], Status = PlayerStatus.Empty, SubAreaId = 0, MapId = 0, WorldY = 0, WorldX = 0, AlignmentSide = 0, Initiative = 0, RegenRate = 0, Prospecting = 0, MaxLifePoints = 0, LifePoints = 0, Rank = 0 };

	public required ushort Rank { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(Rank);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Rank = reader.ReadVarUInt16();
	}
}

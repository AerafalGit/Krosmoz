// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightFighterCompanionLightInformations : GameFightFighterLightInformations
{
	public new const ushort StaticProtocolId = 454;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightFighterCompanionLightInformations Empty =>
		new() { Breed = 0, Level = 0, Wave = 0, Id = 0, Alive = false, Sex = false, CompanionId = 0, MasterId = 0 };

	public required sbyte CompanionId { get; set; }

	public required int MasterId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(CompanionId);
		writer.WriteInt32(MasterId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CompanionId = reader.ReadInt8();
		MasterId = reader.ReadInt32();
	}
}

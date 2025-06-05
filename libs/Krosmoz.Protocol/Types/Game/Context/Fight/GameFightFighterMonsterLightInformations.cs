// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightFighterMonsterLightInformations : GameFightFighterLightInformations
{
	public new const ushort StaticProtocolId = 455;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightFighterMonsterLightInformations Empty =>
		new() { Breed = 0, Level = 0, Wave = 0, Id = 0, Alive = false, Sex = false, CreatureGenericId = 0 };

	public required ushort CreatureGenericId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(CreatureGenericId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CreatureGenericId = reader.ReadVarUInt16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightFighterTaxCollectorLightInformations : GameFightFighterLightInformations
{
	public new const ushort StaticProtocolId = 457;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightFighterTaxCollectorLightInformations Empty =>
		new() { Breed = 0, Level = 0, Wave = 0, Id = 0, Alive = false, Sex = false, FirstNameId = 0, LastNameId = 0 };

	public required ushort FirstNameId { get; set; }

	public required ushort LastNameId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(FirstNameId);
		writer.WriteVarUInt16(LastNameId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		FirstNameId = reader.ReadVarUInt16();
		LastNameId = reader.ReadVarUInt16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character;

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismFightDefenderAddMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5895;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismFightDefenderAddMessage Empty =>
		new() { SubAreaId = 0, FightId = 0, Defender = CharacterMinimalPlusLookInformations.Empty };

	public required ushort SubAreaId { get; set; }

	public required ushort FightId { get; set; }

	public required CharacterMinimalPlusLookInformations Defender { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteVarUInt16(FightId);
		writer.WriteUInt16(Defender.ProtocolId);
		Defender.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadVarUInt16();
		FightId = reader.ReadVarUInt16();
		Defender = Types.TypeFactory.CreateType<CharacterMinimalPlusLookInformations>(reader.ReadUInt16());
		Defender.Deserialize(reader);
	}
}

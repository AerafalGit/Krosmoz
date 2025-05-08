// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight.Character;

public class GameFightShowFighterMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5864;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightShowFighterMessage Empty =>
		new() { Informations = GameFightFighterInformations.Empty };

	public required GameFightFighterInformations Informations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUShort(Informations.ProtocolId);
		Informations.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Informations = Types.TypeFactory.CreateType<GameFightFighterInformations>(reader.ReadUShort());
		Informations.Deserialize(reader);
	}
}

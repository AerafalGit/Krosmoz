// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight.Character;

public sealed class GameFightRefreshFighterMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6309;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightRefreshFighterMessage Empty =>
		new() { Informations = GameContextActorInformations.Empty };

	public required GameContextActorInformations Informations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Informations.ProtocolId);
		Informations.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Informations = Types.TypeFactory.CreateType<GameContextActorInformations>(reader.ReadUInt16());
		Informations.Deserialize(reader);
	}
}

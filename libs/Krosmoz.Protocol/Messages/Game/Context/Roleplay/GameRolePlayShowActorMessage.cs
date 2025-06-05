// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public class GameRolePlayShowActorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5632;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayShowActorMessage Empty =>
		new() { Informations = GameRolePlayActorInformations.Empty };

	public required GameRolePlayActorInformations Informations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Informations.ProtocolId);
		Informations.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Informations = Types.TypeFactory.CreateType<GameRolePlayActorInformations>(reader.ReadUInt16());
		Informations.Deserialize(reader);
	}
}

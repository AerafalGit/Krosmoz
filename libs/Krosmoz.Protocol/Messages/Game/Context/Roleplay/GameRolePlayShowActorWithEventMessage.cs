// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public sealed class GameRolePlayShowActorWithEventMessage : GameRolePlayShowActorMessage
{
	public new const uint StaticProtocolId = 6407;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayShowActorWithEventMessage Empty =>
		new() { Informations = GameRolePlayActorInformations.Empty, ActorEventId = 0 };

	public required sbyte ActorEventId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(ActorEventId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ActorEventId = reader.ReadInt8();
	}
}

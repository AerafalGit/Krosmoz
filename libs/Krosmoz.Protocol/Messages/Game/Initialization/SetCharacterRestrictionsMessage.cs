// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Restriction;

namespace Krosmoz.Protocol.Messages.Game.Initialization;

public sealed class SetCharacterRestrictionsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 170;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SetCharacterRestrictionsMessage Empty =>
		new() { ActorId = 0, Restrictions = ActorRestrictionsInformations.Empty };

	public required int ActorId { get; set; }

	public required ActorRestrictionsInformations Restrictions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(ActorId);
		Restrictions.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ActorId = reader.ReadInt32();
		Restrictions = ActorRestrictionsInformations.Empty;
		Restrictions.Deserialize(reader);
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Subscriber;

public sealed class SubscriptionZoneMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5573;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SubscriptionZoneMessage Empty =>
		new() { Active = false };

	public required bool Active { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Active);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Active = reader.ReadBoolean();
	}
}

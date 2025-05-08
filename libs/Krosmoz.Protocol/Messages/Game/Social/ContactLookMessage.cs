// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Messages.Game.Social;

public sealed class ContactLookMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5934;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ContactLookMessage Empty =>
		new() { RequestId = 0, PlayerName = string.Empty, PlayerId = 0, Look = EntityLook.Empty };

	public required int RequestId { get; set; }

	public required string PlayerName { get; set; }

	public required int PlayerId { get; set; }

	public required EntityLook Look { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(RequestId);
		writer.WriteUtfLengthPrefixed16(PlayerName);
		writer.WriteInt(PlayerId);
		Look.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RequestId = reader.ReadInt();
		PlayerName = reader.ReadUtfLengthPrefixed16();
		PlayerId = reader.ReadInt();
		Look = EntityLook.Empty;
		Look.Deserialize(reader);
	}
}

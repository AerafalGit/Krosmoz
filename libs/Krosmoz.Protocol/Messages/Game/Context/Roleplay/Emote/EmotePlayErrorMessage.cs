// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Emote;

public sealed class EmotePlayErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5688;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static EmotePlayErrorMessage Empty =>
		new() { EmoteId = 0 };

	public required byte EmoteId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt8(EmoteId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		EmoteId = reader.ReadUInt8();
	}
}

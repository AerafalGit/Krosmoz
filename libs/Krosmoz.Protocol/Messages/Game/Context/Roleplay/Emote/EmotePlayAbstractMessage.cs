// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Emote;

public class EmotePlayAbstractMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5690;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static EmotePlayAbstractMessage Empty =>
		new() { EmoteId = 0, EmoteStartTime = 0 };

	public required byte EmoteId { get; set; }

	public required double EmoteStartTime { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt8(EmoteId);
		writer.WriteDouble(EmoteStartTime);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		EmoteId = reader.ReadUInt8();
		EmoteStartTime = reader.ReadDouble();
	}
}

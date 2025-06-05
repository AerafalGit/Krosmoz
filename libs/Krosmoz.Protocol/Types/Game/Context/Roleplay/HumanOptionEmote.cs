// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class HumanOptionEmote : HumanOption
{
	public new const ushort StaticProtocolId = 407;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new HumanOptionEmote Empty =>
		new() { EmoteId = 0, EmoteStartTime = 0 };

	public required byte EmoteId { get; set; }

	public required double EmoteStartTime { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt8(EmoteId);
		writer.WriteDouble(EmoteStartTime);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		EmoteId = reader.ReadUInt8();
		EmoteStartTime = reader.ReadDouble();
	}
}

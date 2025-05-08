// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildChangeMemberParametersMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5549;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildChangeMemberParametersMessage Empty =>
		new() { MemberId = 0, Rank = 0, ExperienceGivenPercent = 0, Rights = 0 };

	public required int MemberId { get; set; }

	public required short Rank { get; set; }

	public required sbyte ExperienceGivenPercent { get; set; }

	public required uint Rights { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(MemberId);
		writer.WriteShort(Rank);
		writer.WriteSByte(ExperienceGivenPercent);
		writer.WriteUInt(Rights);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MemberId = reader.ReadInt();
		Rank = reader.ReadShort();
		ExperienceGivenPercent = reader.ReadSByte();
		Rights = reader.ReadUInt();
	}
}

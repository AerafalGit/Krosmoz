// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Achievement;

public sealed class AchievementFinishedInformationMessage : AchievementFinishedMessage
{
	public new const uint StaticProtocolId = 6381;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new AchievementFinishedInformationMessage Empty =>
		new() { Finishedlevel = 0, Id = 0, Name = string.Empty, PlayerId = 0 };

	public required string Name { get; set; }

	public required uint PlayerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(Name);
		writer.WriteVarUInt32(PlayerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Name = reader.ReadUtfPrefixedLength16();
		PlayerId = reader.ReadVarUInt32();
	}
}

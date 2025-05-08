// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Stats;

public sealed class CharacterExperienceGainMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6321;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterExperienceGainMessage Empty =>
		new() { ExperienceCharacter = 0, ExperienceMount = 0, ExperienceGuild = 0, ExperienceIncarnation = 0 };

	public required double ExperienceCharacter { get; set; }

	public required double ExperienceMount { get; set; }

	public required double ExperienceGuild { get; set; }

	public required double ExperienceIncarnation { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteDouble(ExperienceCharacter);
		writer.WriteDouble(ExperienceMount);
		writer.WriteDouble(ExperienceGuild);
		writer.WriteDouble(ExperienceIncarnation);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ExperienceCharacter = reader.ReadDouble();
		ExperienceMount = reader.ReadDouble();
		ExperienceGuild = reader.ReadDouble();
		ExperienceIncarnation = reader.ReadDouble();
	}
}

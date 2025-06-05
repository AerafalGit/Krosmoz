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

	public required ulong ExperienceCharacter { get; set; }

	public required ulong ExperienceMount { get; set; }

	public required ulong ExperienceGuild { get; set; }

	public required ulong ExperienceIncarnation { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt64(ExperienceCharacter);
		writer.WriteVarUInt64(ExperienceMount);
		writer.WriteVarUInt64(ExperienceGuild);
		writer.WriteVarUInt64(ExperienceIncarnation);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ExperienceCharacter = reader.ReadVarUInt64();
		ExperienceMount = reader.ReadVarUInt64();
		ExperienceGuild = reader.ReadVarUInt64();
		ExperienceIncarnation = reader.ReadVarUInt64();
	}
}

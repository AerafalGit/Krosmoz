// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class FightTeamMemberCharacterInformations : FightTeamMemberInformations
{
	public new const ushort StaticProtocolId = 13;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightTeamMemberCharacterInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0 };

	public required string Name { get; set; }

	public required short Level { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfLengthPrefixed16(Name);
		writer.WriteShort(Level);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Name = reader.ReadUtfLengthPrefixed16();
		Level = reader.ReadShort();
	}
}

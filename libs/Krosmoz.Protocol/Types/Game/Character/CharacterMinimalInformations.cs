// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character;

public class CharacterMinimalInformations : AbstractCharacterInformation
{
	public new const ushort StaticProtocolId = 110;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterMinimalInformations Empty =>
		new() { Id = 0, Level = 0, Name = string.Empty };

	public required byte Level { get; set; }

	public required string Name { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt8(Level);
		writer.WriteUtfPrefixedLength16(Name);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Level = reader.ReadUInt8();
		Name = reader.ReadUtfPrefixedLength16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Stats;

public sealed class CharacterLevelUpInformationMessage : CharacterLevelUpMessage
{
	public new const uint StaticProtocolId = 6076;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharacterLevelUpInformationMessage Empty =>
		new() { NewLevel = 0, Name = string.Empty, Id = 0 };

	public required string Name { get; set; }

	public required uint Id { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(Name);
		writer.WriteVarUInt32(Id);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Name = reader.ReadUtfPrefixedLength16();
		Id = reader.ReadVarUInt32();
	}
}

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

	public required int Id { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfLengthPrefixed16(Name);
		writer.WriteInt(Id);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Name = reader.ReadUtfLengthPrefixed16();
		Id = reader.ReadInt();
	}
}

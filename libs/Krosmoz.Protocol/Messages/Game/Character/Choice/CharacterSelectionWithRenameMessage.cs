// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharacterSelectionWithRenameMessage : CharacterSelectionMessage
{
	public new const uint StaticProtocolId = 6121;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharacterSelectionWithRenameMessage Empty =>
		new() { Id = 0, Name = string.Empty };

	public required string Name { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfLengthPrefixed16(Name);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Name = reader.ReadUtfLengthPrefixed16();
	}
}

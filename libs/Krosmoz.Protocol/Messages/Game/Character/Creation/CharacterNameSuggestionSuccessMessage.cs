// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Creation;

public sealed class CharacterNameSuggestionSuccessMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5544;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterNameSuggestionSuccessMessage Empty =>
		new() { Suggestion = string.Empty };

	public required string Suggestion { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Suggestion);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Suggestion = reader.ReadUtfPrefixedLength16();
	}
}

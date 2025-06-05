// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Deletion;

public sealed class CharacterDeletionRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 165;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterDeletionRequestMessage Empty =>
		new() { CharacterId = 0, SecretAnswerHash = string.Empty };

	public required int CharacterId { get; set; }

	public required string SecretAnswerHash { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(CharacterId);
		writer.WriteUtfPrefixedLength16(SecretAnswerHash);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CharacterId = reader.ReadInt32();
		SecretAnswerHash = reader.ReadUtfPrefixedLength16();
	}
}

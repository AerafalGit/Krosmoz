// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Replay;

public sealed class CharacterReplayWithRenameRequestMessage : CharacterReplayRequestMessage
{
	public new const uint StaticProtocolId = 6122;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharacterReplayWithRenameRequestMessage Empty =>
		new() { CharacterId = 0, Name = string.Empty };

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

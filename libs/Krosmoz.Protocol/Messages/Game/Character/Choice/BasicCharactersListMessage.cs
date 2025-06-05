// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Choice;

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public class BasicCharactersListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6475;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicCharactersListMessage Empty =>
		new() { Characters = [] };

	public required IEnumerable<CharacterBaseInformations> Characters { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var charactersBefore = writer.Position;
		var charactersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Characters)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			charactersCount++;
		}
		var charactersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, charactersBefore);
		writer.WriteInt16((short)charactersCount);
		writer.Seek(SeekOrigin.Begin, charactersAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var charactersCount = reader.ReadInt16();
		var characters = new CharacterBaseInformations[charactersCount];
		for (var i = 0; i < charactersCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<CharacterBaseInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			characters[i] = entry;
		}
		Characters = characters;
	}
}

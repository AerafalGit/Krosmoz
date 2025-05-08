// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Choice;

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public class CharactersListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 151;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharactersListMessage Empty =>
		new() { HasStartupActions = false, Characters = [] };

	public required bool HasStartupActions { get; set; }

	public required IEnumerable<CharacterBaseInformations> Characters { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(HasStartupActions);
		var charactersBefore = writer.Position;
		var charactersCount = 0;
		writer.WriteShort(0);
		foreach (var item in Characters)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			charactersCount++;
		}
		var charactersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, charactersBefore);
		writer.WriteShort((short)charactersCount);
		writer.Seek(SeekOrigin.Begin, charactersAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HasStartupActions = reader.ReadBoolean();
		var charactersCount = reader.ReadShort();
		var characters = new CharacterBaseInformations[charactersCount];
		for (var i = 0; i < charactersCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<CharacterBaseInformations>(reader.ReadUShort());
			entry.Deserialize(reader);
			characters[i] = entry;
		}
		Characters = characters;
	}
}

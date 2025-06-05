// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Choice;

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharactersListWithRemodelingMessage : CharactersListMessage
{
	public new const uint StaticProtocolId = 6550;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharactersListWithRemodelingMessage Empty =>
		new() { Characters = [], HasStartupActions = false, CharactersToRemodel = [] };

	public required IEnumerable<CharacterToRemodelInformations> CharactersToRemodel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var charactersToRemodelBefore = writer.Position;
		var charactersToRemodelCount = 0;
		writer.WriteInt16(0);
		foreach (var item in CharactersToRemodel)
		{
			item.Serialize(writer);
			charactersToRemodelCount++;
		}
		var charactersToRemodelAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, charactersToRemodelBefore);
		writer.WriteInt16((short)charactersToRemodelCount);
		writer.Seek(SeekOrigin.Begin, charactersToRemodelAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var charactersToRemodelCount = reader.ReadInt16();
		var charactersToRemodel = new CharacterToRemodelInformations[charactersToRemodelCount];
		for (var i = 0; i < charactersToRemodelCount; i++)
		{
			var entry = CharacterToRemodelInformations.Empty;
			entry.Deserialize(reader);
			charactersToRemodel[i] = entry;
		}
		CharactersToRemodel = charactersToRemodel;
	}
}

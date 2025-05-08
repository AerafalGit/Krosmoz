// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Choice;

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharactersListWithModificationsMessage : CharactersListMessage
{
	public new const uint StaticProtocolId = 6120;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharactersListWithModificationsMessage Empty =>
		new() { Characters = [], HasStartupActions = false, CharactersToRecolor = [], CharactersToRename = [], UnusableCharacters = [], CharactersToRelook = [] };

	public required IEnumerable<CharacterToRecolorInformation> CharactersToRecolor { get; set; }

	public required IEnumerable<int> CharactersToRename { get; set; }

	public required IEnumerable<int> UnusableCharacters { get; set; }

	public required IEnumerable<CharacterToRelookInformation> CharactersToRelook { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var charactersToRecolorBefore = writer.Position;
		var charactersToRecolorCount = 0;
		writer.WriteShort(0);
		foreach (var item in CharactersToRecolor)
		{
			item.Serialize(writer);
			charactersToRecolorCount++;
		}
		var charactersToRecolorAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, charactersToRecolorBefore);
		writer.WriteShort((short)charactersToRecolorCount);
		writer.Seek(SeekOrigin.Begin, charactersToRecolorAfter);
		var charactersToRenameBefore = writer.Position;
		var charactersToRenameCount = 0;
		writer.WriteShort(0);
		foreach (var item in CharactersToRename)
		{
			writer.WriteInt(item);
			charactersToRenameCount++;
		}
		var charactersToRenameAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, charactersToRenameBefore);
		writer.WriteShort((short)charactersToRenameCount);
		writer.Seek(SeekOrigin.Begin, charactersToRenameAfter);
		var unusableCharactersBefore = writer.Position;
		var unusableCharactersCount = 0;
		writer.WriteShort(0);
		foreach (var item in UnusableCharacters)
		{
			writer.WriteInt(item);
			unusableCharactersCount++;
		}
		var unusableCharactersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, unusableCharactersBefore);
		writer.WriteShort((short)unusableCharactersCount);
		writer.Seek(SeekOrigin.Begin, unusableCharactersAfter);
		var charactersToRelookBefore = writer.Position;
		var charactersToRelookCount = 0;
		writer.WriteShort(0);
		foreach (var item in CharactersToRelook)
		{
			item.Serialize(writer);
			charactersToRelookCount++;
		}
		var charactersToRelookAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, charactersToRelookBefore);
		writer.WriteShort((short)charactersToRelookCount);
		writer.Seek(SeekOrigin.Begin, charactersToRelookAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var charactersToRecolorCount = reader.ReadShort();
		var charactersToRecolor = new CharacterToRecolorInformation[charactersToRecolorCount];
		for (var i = 0; i < charactersToRecolorCount; i++)
		{
			var entry = CharacterToRecolorInformation.Empty;
			entry.Deserialize(reader);
			charactersToRecolor[i] = entry;
		}
		CharactersToRecolor = charactersToRecolor;
		var charactersToRenameCount = reader.ReadShort();
		var charactersToRename = new int[charactersToRenameCount];
		for (var i = 0; i < charactersToRenameCount; i++)
		{
			charactersToRename[i] = reader.ReadInt();
		}
		CharactersToRename = charactersToRename;
		var unusableCharactersCount = reader.ReadShort();
		var unusableCharacters = new int[unusableCharactersCount];
		for (var i = 0; i < unusableCharactersCount; i++)
		{
			unusableCharacters[i] = reader.ReadInt();
		}
		UnusableCharacters = unusableCharacters;
		var charactersToRelookCount = reader.ReadShort();
		var charactersToRelook = new CharacterToRelookInformation[charactersToRelookCount];
		for (var i = 0; i < charactersToRelookCount; i++)
		{
			var entry = CharacterToRelookInformation.Empty;
			entry.Deserialize(reader);
			charactersToRelook[i] = entry;
		}
		CharactersToRelook = charactersToRelook;
	}
}

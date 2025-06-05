// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Authorized;

public sealed class ConsoleCommandsListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6127;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ConsoleCommandsListMessage Empty =>
		new() { Aliases = [], Args = [], Descriptions = [] };

	public required IEnumerable<string> Aliases { get; set; }

	public required IEnumerable<string> Args { get; set; }

	public required IEnumerable<string> Descriptions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var aliasesBefore = writer.Position;
		var aliasesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Aliases)
		{
			writer.WriteUtfPrefixedLength16(item);
			aliasesCount++;
		}
		var aliasesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, aliasesBefore);
		writer.WriteInt16((short)aliasesCount);
		writer.Seek(SeekOrigin.Begin, aliasesAfter);
		var argsBefore = writer.Position;
		var argsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Args)
		{
			writer.WriteUtfPrefixedLength16(item);
			argsCount++;
		}
		var argsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, argsBefore);
		writer.WriteInt16((short)argsCount);
		writer.Seek(SeekOrigin.Begin, argsAfter);
		var descriptionsBefore = writer.Position;
		var descriptionsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Descriptions)
		{
			writer.WriteUtfPrefixedLength16(item);
			descriptionsCount++;
		}
		var descriptionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, descriptionsBefore);
		writer.WriteInt16((short)descriptionsCount);
		writer.Seek(SeekOrigin.Begin, descriptionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var aliasesCount = reader.ReadInt16();
		var aliases = new string[aliasesCount];
		for (var i = 0; i < aliasesCount; i++)
		{
			aliases[i] = reader.ReadUtfPrefixedLength16();
		}
		Aliases = aliases;
		var argsCount = reader.ReadInt16();
		var args = new string[argsCount];
		for (var i = 0; i < argsCount; i++)
		{
			args[i] = reader.ReadUtfPrefixedLength16();
		}
		Args = args;
		var descriptionsCount = reader.ReadInt16();
		var descriptions = new string[descriptionsCount];
		for (var i = 0; i < descriptionsCount; i++)
		{
			descriptions[i] = reader.ReadUtfPrefixedLength16();
		}
		Descriptions = descriptions;
	}
}

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
		new() { Aliases = [], Arguments = [], Descriptions = [] };

	public required IEnumerable<string> Aliases { get; set; }

	public required IEnumerable<string> Arguments { get; set; }

	public required IEnumerable<string> Descriptions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var aliasesBefore = writer.Position;
		var aliasesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Aliases)
		{
			writer.WriteUtfLengthPrefixed16(item);
			aliasesCount++;
		}
		var aliasesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, aliasesBefore);
		writer.WriteShort((short)aliasesCount);
		writer.Seek(SeekOrigin.Begin, aliasesAfter);
		var argumentsBefore = writer.Position;
		var argumentsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Arguments)
		{
			writer.WriteUtfLengthPrefixed16(item);
			argumentsCount++;
		}
		var argumentsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, argumentsBefore);
		writer.WriteShort((short)argumentsCount);
		writer.Seek(SeekOrigin.Begin, argumentsAfter);
		var descriptionsBefore = writer.Position;
		var descriptionsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Descriptions)
		{
			writer.WriteUtfLengthPrefixed16(item);
			descriptionsCount++;
		}
		var descriptionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, descriptionsBefore);
		writer.WriteShort((short)descriptionsCount);
		writer.Seek(SeekOrigin.Begin, descriptionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var aliasesCount = reader.ReadShort();
		var aliases = new string[aliasesCount];
		for (var i = 0; i < aliasesCount; i++)
		{
			aliases[i] = reader.ReadUtfLengthPrefixed16();
		}
		Aliases = aliases;
		var argumentsCount = reader.ReadShort();
		var arguments = new string[argumentsCount];
		for (var i = 0; i < argumentsCount; i++)
		{
			arguments[i] = reader.ReadUtfLengthPrefixed16();
		}
		Arguments = arguments;
		var descriptionsCount = reader.ReadShort();
		var descriptions = new string[descriptionsCount];
		for (var i = 0; i < descriptionsCount; i++)
		{
			descriptions[i] = reader.ReadUtfLengthPrefixed16();
		}
		Descriptions = descriptions;
	}
}

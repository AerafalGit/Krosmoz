// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Approach;

namespace Krosmoz.Protocol.Messages.Game.Approach;

public sealed class ServerSessionConstantsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6434;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ServerSessionConstantsMessage Empty =>
		new() { Variables = [] };

	public required IEnumerable<ServerSessionConstant> Variables { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var variablesBefore = writer.Position;
		var variablesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Variables)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			variablesCount++;
		}
		var variablesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, variablesBefore);
		writer.WriteInt16((short)variablesCount);
		writer.Seek(SeekOrigin.Begin, variablesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var variablesCount = reader.ReadInt16();
		var variables = new ServerSessionConstant[variablesCount];
		for (var i = 0; i < variablesCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<ServerSessionConstant>(reader.ReadUInt16());
			entry.Deserialize(reader);
			variables[i] = entry;
		}
		Variables = variables;
	}
}

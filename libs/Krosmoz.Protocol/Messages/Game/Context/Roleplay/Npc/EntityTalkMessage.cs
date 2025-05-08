// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public sealed class EntityTalkMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6110;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static EntityTalkMessage Empty =>
		new() { EntityId = 0, TextId = 0, Parameters = [] };

	public required int EntityId { get; set; }

	public required short TextId { get; set; }

	public required IEnumerable<string> Parameters { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(EntityId);
		writer.WriteShort(TextId);
		var parametersBefore = writer.Position;
		var parametersCount = 0;
		writer.WriteShort(0);
		foreach (var item in Parameters)
		{
			writer.WriteUtfLengthPrefixed16(item);
			parametersCount++;
		}
		var parametersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, parametersBefore);
		writer.WriteShort((short)parametersCount);
		writer.Seek(SeekOrigin.Begin, parametersAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		EntityId = reader.ReadInt();
		TextId = reader.ReadShort();
		var parametersCount = reader.ReadShort();
		var parameters = new string[parametersCount];
		for (var i = 0; i < parametersCount; i++)
		{
			parameters[i] = reader.ReadUtfLengthPrefixed16();
		}
		Parameters = parameters;
	}
}

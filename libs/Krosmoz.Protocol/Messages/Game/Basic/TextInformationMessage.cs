// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class TextInformationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 780;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TextInformationMessage Empty =>
		new() { MsgType = 0, MsgId = 0, Parameters = [] };

	public required sbyte MsgType { get; set; }

	public required ushort MsgId { get; set; }

	public required IEnumerable<string> Parameters { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(MsgType);
		writer.WriteVarUInt16(MsgId);
		var parametersBefore = writer.Position;
		var parametersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Parameters)
		{
			writer.WriteUtfPrefixedLength16(item);
			parametersCount++;
		}
		var parametersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, parametersBefore);
		writer.WriteInt16((short)parametersCount);
		writer.Seek(SeekOrigin.Begin, parametersAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MsgType = reader.ReadInt8();
		MsgId = reader.ReadVarUInt16();
		var parametersCount = reader.ReadInt16();
		var parameters = new string[parametersCount];
		for (var i = 0; i < parametersCount; i++)
		{
			parameters[i] = reader.ReadUtfPrefixedLength16();
		}
		Parameters = parameters;
	}
}

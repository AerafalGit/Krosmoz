// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Security;

public sealed class CheckIntegrityMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6372;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CheckIntegrityMessage Empty =>
		new() { Data = [] };

	public required IEnumerable<sbyte> Data { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var dataBefore = writer.Position;
		var dataCount = 0;
		writer.WriteVarInt32(0);
		foreach (var item in Data)
		{
			writer.WriteInt8(item);
			dataCount++;
		}
		var dataAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, dataBefore);
		writer.WriteVarInt32(dataCount);
		writer.Seek(SeekOrigin.Begin, dataAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var dataCount = reader.ReadVarInt32();
		var data = new sbyte[dataCount];
		for (var i = 0; i < dataCount; i++)
		{
			data[i] = reader.ReadInt8();
		}
		Data = data;
	}
}

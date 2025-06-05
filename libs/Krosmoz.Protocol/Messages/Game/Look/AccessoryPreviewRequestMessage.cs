// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Look;

public sealed class AccessoryPreviewRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6518;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AccessoryPreviewRequestMessage Empty =>
		new() { GenericId = [] };

	public required IEnumerable<ushort> GenericId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var genericIdBefore = writer.Position;
		var genericIdCount = 0;
		writer.WriteInt16(0);
		foreach (var item in GenericId)
		{
			writer.WriteVarUInt16(item);
			genericIdCount++;
		}
		var genericIdAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, genericIdBefore);
		writer.WriteInt16((short)genericIdCount);
		writer.Seek(SeekOrigin.Begin, genericIdAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var genericIdCount = reader.ReadInt16();
		var genericId = new ushort[genericIdCount];
		for (var i = 0; i < genericIdCount; i++)
		{
			genericId[i] = reader.ReadVarUInt16();
		}
		GenericId = genericId;
	}
}

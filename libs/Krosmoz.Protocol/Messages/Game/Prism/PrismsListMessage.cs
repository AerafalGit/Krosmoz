// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Prism;

namespace Krosmoz.Protocol.Messages.Game.Prism;

public class PrismsListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6440;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismsListMessage Empty =>
		new() { Prisms = [] };

	public required IEnumerable<PrismSubareaEmptyInfo> Prisms { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var prismsBefore = writer.Position;
		var prismsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Prisms)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			prismsCount++;
		}
		var prismsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, prismsBefore);
		writer.WriteShort((short)prismsCount);
		writer.Seek(SeekOrigin.Begin, prismsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var prismsCount = reader.ReadShort();
		var prisms = new PrismSubareaEmptyInfo[prismsCount];
		for (var i = 0; i < prismsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<PrismSubareaEmptyInfo>(reader.ReadUShort());
			entry.Deserialize(reader);
			prisms[i] = entry;
		}
		Prisms = prisms;
	}
}

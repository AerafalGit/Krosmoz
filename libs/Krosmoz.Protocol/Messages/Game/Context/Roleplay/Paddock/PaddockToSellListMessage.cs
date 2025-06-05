// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Paddock;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Paddock;

public sealed class PaddockToSellListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6138;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PaddockToSellListMessage Empty =>
		new() { PageIndex = 0, TotalPage = 0, PaddockList = [] };

	public required ushort PageIndex { get; set; }

	public required ushort TotalPage { get; set; }

	public required IEnumerable<PaddockInformationsForSell> PaddockList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(PageIndex);
		writer.WriteVarUInt16(TotalPage);
		var paddockListBefore = writer.Position;
		var paddockListCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PaddockList)
		{
			item.Serialize(writer);
			paddockListCount++;
		}
		var paddockListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, paddockListBefore);
		writer.WriteInt16((short)paddockListCount);
		writer.Seek(SeekOrigin.Begin, paddockListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PageIndex = reader.ReadVarUInt16();
		TotalPage = reader.ReadVarUInt16();
		var paddockListCount = reader.ReadInt16();
		var paddockList = new PaddockInformationsForSell[paddockListCount];
		for (var i = 0; i < paddockListCount; i++)
		{
			var entry = PaddockInformationsForSell.Empty;
			entry.Deserialize(reader);
			paddockList[i] = entry;
		}
		PaddockList = paddockList;
	}
}

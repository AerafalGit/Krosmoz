// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Web.Krosmaster;

namespace Krosmoz.Protocol.Messages.Web.Krosmaster;

public sealed class KrosmasterInventoryMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6350;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static KrosmasterInventoryMessage Empty =>
		new() { Figures = [] };

	public required IEnumerable<KrosmasterFigure> Figures { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var figuresBefore = writer.Position;
		var figuresCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Figures)
		{
			item.Serialize(writer);
			figuresCount++;
		}
		var figuresAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, figuresBefore);
		writer.WriteInt16((short)figuresCount);
		writer.Seek(SeekOrigin.Begin, figuresAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var figuresCount = reader.ReadInt16();
		var figures = new KrosmasterFigure[figuresCount];
		for (var i = 0; i < figuresCount; i++)
		{
			var entry = KrosmasterFigure.Empty;
			entry.Deserialize(reader);
			figures[i] = entry;
		}
		Figures = figures;
	}
}

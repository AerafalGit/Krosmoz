// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Guild;

public sealed class GuildEmblem : DofusType
{
	public new const ushort StaticProtocolId = 87;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GuildEmblem Empty =>
		new() { SymbolShape = 0, SymbolColor = 0, BackgroundShape = 0, BackgroundColor = 0 };

	public required short SymbolShape { get; set; }

	public required int SymbolColor { get; set; }

	public required short BackgroundShape { get; set; }

	public required int BackgroundColor { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(SymbolShape);
		writer.WriteInt32(SymbolColor);
		writer.WriteInt16(BackgroundShape);
		writer.WriteInt32(BackgroundColor);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SymbolShape = reader.ReadInt16();
		SymbolColor = reader.ReadInt32();
		BackgroundShape = reader.ReadInt16();
		BackgroundColor = reader.ReadInt32();
	}
}

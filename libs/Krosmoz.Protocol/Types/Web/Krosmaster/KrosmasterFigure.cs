// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Web.Krosmaster;

public sealed class KrosmasterFigure : DofusType
{
	public new const ushort StaticProtocolId = 397;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static KrosmasterFigure Empty =>
		new() { Uid = string.Empty, Figure = 0, Pedestal = 0, Bound = false };

	public required string Uid { get; set; }

	public required ushort Figure { get; set; }

	public required ushort Pedestal { get; set; }

	public required bool Bound { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Uid);
		writer.WriteVarUInt16(Figure);
		writer.WriteVarUInt16(Pedestal);
		writer.WriteBoolean(Bound);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Uid = reader.ReadUtfPrefixedLength16();
		Figure = reader.ReadVarUInt16();
		Pedestal = reader.ReadVarUInt16();
		Bound = reader.ReadBoolean();
	}
}

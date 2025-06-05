// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Idol;

public class Idol : DofusType
{
	public new const ushort StaticProtocolId = 489;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static Idol Empty =>
		new() { Id = 0, XpBonusPercent = 0, DropBonusPercent = 0 };

	public required ushort Id { get; set; }

	public required ushort XpBonusPercent { get; set; }

	public required ushort DropBonusPercent { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Id);
		writer.WriteVarUInt16(XpBonusPercent);
		writer.WriteVarUInt16(DropBonusPercent);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadVarUInt16();
		XpBonusPercent = reader.ReadVarUInt16();
		DropBonusPercent = reader.ReadVarUInt16();
	}
}

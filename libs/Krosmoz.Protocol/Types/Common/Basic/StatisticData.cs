// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Common.Basic;

public class StatisticData : DofusType
{
	public new const ushort StaticProtocolId = 484;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static StatisticData Empty =>
		new() { ActionId = 0 };

	public required ushort ActionId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ActionId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ActionId = reader.ReadVarUInt16();
	}
}

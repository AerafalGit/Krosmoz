// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Common.Basic;

public sealed class StatisticDataByte : StatisticData
{
	public new const ushort StaticProtocolId = 486;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new StatisticDataByte Empty =>
		new() { ActionId = 0, Value = 0 };

	public required sbyte Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Value);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Value = reader.ReadInt8();
	}
}

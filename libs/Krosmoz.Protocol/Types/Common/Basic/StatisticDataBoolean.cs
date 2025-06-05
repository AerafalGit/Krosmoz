// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Common.Basic;

public sealed class StatisticDataBoolean : StatisticData
{
	public new const ushort StaticProtocolId = 482;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new StatisticDataBoolean Empty =>
		new() { ActionId = 0, Value = false };

	public required bool Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(Value);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Value = reader.ReadBoolean();
	}
}

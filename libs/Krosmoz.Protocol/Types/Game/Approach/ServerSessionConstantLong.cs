// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Approach;

public sealed class ServerSessionConstantLong : ServerSessionConstant
{
	public new const ushort StaticProtocolId = 429;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ServerSessionConstantLong Empty =>
		new() { Id = 0, Value = 0 };

	public required double Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(Value);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Value = reader.ReadDouble();
	}
}

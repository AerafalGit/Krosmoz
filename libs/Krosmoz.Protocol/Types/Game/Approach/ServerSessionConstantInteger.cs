// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Approach;

public sealed class ServerSessionConstantInteger : ServerSessionConstant
{
	public new const ushort StaticProtocolId = 433;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ServerSessionConstantInteger Empty =>
		new() { Id = 0, Value = 0 };

	public required int Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(Value);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Value = reader.ReadInt32();
	}
}

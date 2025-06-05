// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Approach;

public class ServerSessionConstant : DofusType
{
	public new const ushort StaticProtocolId = 430;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static ServerSessionConstant Empty =>
		new() { Id = 0 };

	public required ushort Id { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Id);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadVarUInt16();
	}
}

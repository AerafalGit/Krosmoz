// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismFightRemovedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6453;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismFightRemovedMessage Empty =>
		new() { SubAreaId = 0 };

	public required ushort SubAreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadVarUInt16();
	}
}

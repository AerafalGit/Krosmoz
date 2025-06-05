// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismFightSwapRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5901;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismFightSwapRequestMessage Empty =>
		new() { SubAreaId = 0, TargetId = 0 };

	public required ushort SubAreaId { get; set; }

	public required uint TargetId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteVarUInt32(TargetId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadVarUInt16();
		TargetId = reader.ReadVarUInt32();
	}
}

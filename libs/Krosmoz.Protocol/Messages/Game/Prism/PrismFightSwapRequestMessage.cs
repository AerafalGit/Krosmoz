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

	public required short SubAreaId { get; set; }

	public required int TargetId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(SubAreaId);
		writer.WriteInt(TargetId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadShort();
		TargetId = reader.ReadInt();
	}
}

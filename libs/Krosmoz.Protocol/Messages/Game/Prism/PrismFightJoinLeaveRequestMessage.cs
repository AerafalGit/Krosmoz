// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismFightJoinLeaveRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5843;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismFightJoinLeaveRequestMessage Empty =>
		new() { SubAreaId = 0, Join = false };

	public required ushort SubAreaId { get; set; }

	public required bool Join { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteBoolean(Join);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadVarUInt16();
		Join = reader.ReadBoolean();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.TreasureHunt;

public sealed class PortalUseRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6492;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PortalUseRequestMessage Empty =>
		new() { PortalId = 0 };

	public required uint PortalId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(PortalId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PortalId = reader.ReadVarUInt32();
	}
}

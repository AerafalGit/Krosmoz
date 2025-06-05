// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.TreasureHunt;

public sealed class PortalInformation : DofusType
{
	public new const ushort StaticProtocolId = 466;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static PortalInformation Empty =>
		new() { PortalId = 0, AreaId = 0 };

	public required ushort PortalId { get; set; }

	public required short AreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(PortalId);
		writer.WriteInt16(AreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PortalId = reader.ReadVarUInt16();
		AreaId = reader.ReadInt16();
	}
}

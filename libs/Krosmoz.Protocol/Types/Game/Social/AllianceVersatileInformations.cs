// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Social;

public sealed class AllianceVersatileInformations : DofusType
{
	public new const ushort StaticProtocolId = 432;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AllianceVersatileInformations Empty =>
		new() { AllianceId = 0, NbGuilds = 0, NbMembers = 0, NbSubarea = 0 };

	public required uint AllianceId { get; set; }

	public required ushort NbGuilds { get; set; }

	public required ushort NbMembers { get; set; }

	public required ushort NbSubarea { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(AllianceId);
		writer.WriteVarUInt16(NbGuilds);
		writer.WriteVarUInt16(NbMembers);
		writer.WriteVarUInt16(NbSubarea);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AllianceId = reader.ReadVarUInt32();
		NbGuilds = reader.ReadVarUInt16();
		NbMembers = reader.ReadVarUInt16();
		NbSubarea = reader.ReadVarUInt16();
	}
}

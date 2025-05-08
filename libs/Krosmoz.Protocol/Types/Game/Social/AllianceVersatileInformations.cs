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

	public required int AllianceId { get; set; }

	public required short NbGuilds { get; set; }

	public required short NbMembers { get; set; }

	public required short NbSubarea { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(AllianceId);
		writer.WriteShort(NbGuilds);
		writer.WriteShort(NbMembers);
		writer.WriteShort(NbSubarea);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AllianceId = reader.ReadInt();
		NbGuilds = reader.ReadShort();
		NbMembers = reader.ReadShort();
		NbSubarea = reader.ReadShort();
	}
}

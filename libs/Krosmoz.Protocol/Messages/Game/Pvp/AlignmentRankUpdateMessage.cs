// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Pvp;

public sealed class AlignmentRankUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6058;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AlignmentRankUpdateMessage Empty =>
		new() { AlignmentRank = 0, Verbose = false };

	public required sbyte AlignmentRank { get; set; }

	public required bool Verbose { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(AlignmentRank);
		writer.WriteBoolean(Verbose);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AlignmentRank = reader.ReadInt8();
		Verbose = reader.ReadBoolean();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

public sealed class DungeonPartyFinderPlayer : DofusType
{
	public new const ushort StaticProtocolId = 373;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static DungeonPartyFinderPlayer Empty =>
		new() { PlayerId = 0, PlayerName = string.Empty, Breed = 0, Sex = false, Level = 0 };

	public required int PlayerId { get; set; }

	public required string PlayerName { get; set; }

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public required short Level { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(PlayerId);
		writer.WriteUtfLengthPrefixed16(PlayerName);
		writer.WriteSByte(Breed);
		writer.WriteBoolean(Sex);
		writer.WriteShort(Level);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PlayerId = reader.ReadInt();
		PlayerName = reader.ReadUtfLengthPrefixed16();
		Breed = reader.ReadSByte();
		Sex = reader.ReadBoolean();
		Level = reader.ReadShort();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Friend;

public sealed class IgnoredOnlineInformations : IgnoredInformations
{
	public new const ushort StaticProtocolId = 105;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new IgnoredOnlineInformations Empty =>
		new() { AccountName = string.Empty, AccountId = 0, PlayerId = 0, PlayerName = string.Empty, Breed = 0, Sex = false };

	public required uint PlayerId { get; set; }

	public required string PlayerName { get; set; }

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(PlayerId);
		writer.WriteUtfPrefixedLength16(PlayerName);
		writer.WriteInt8(Breed);
		writer.WriteBoolean(Sex);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PlayerId = reader.ReadVarUInt32();
		PlayerName = reader.ReadUtfPrefixedLength16();
		Breed = reader.ReadInt8();
		Sex = reader.ReadBoolean();
	}
}

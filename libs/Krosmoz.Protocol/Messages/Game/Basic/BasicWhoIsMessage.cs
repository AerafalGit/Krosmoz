// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Social;

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicWhoIsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 180;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicWhoIsMessage Empty =>
		new() { Self = false, Verbose = false, Position = 0, AccountNickname = string.Empty, AccountId = 0, PlayerName = string.Empty, PlayerId = 0, AreaId = 0, SocialGroups = [], PlayerState = 0 };

	public required bool Self { get; set; }

	public required bool Verbose { get; set; }

	public required sbyte Position { get; set; }

	public required string AccountNickname { get; set; }

	public required int AccountId { get; set; }

	public required string PlayerName { get; set; }

	public required uint PlayerId { get; set; }

	public required short AreaId { get; set; }

	public required IEnumerable<AbstractSocialGroupInfos> SocialGroups { get; set; }

	public required sbyte PlayerState { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, Self);
		flag = BooleanByteWrapper.SetFlag(flag, 1, Verbose);
		writer.WriteUInt8(flag);
		writer.WriteInt8(Position);
		writer.WriteUtfPrefixedLength16(AccountNickname);
		writer.WriteInt32(AccountId);
		writer.WriteUtfPrefixedLength16(PlayerName);
		writer.WriteVarUInt32(PlayerId);
		writer.WriteInt16(AreaId);
		var socialGroupsBefore = writer.Position;
		var socialGroupsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SocialGroups)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			socialGroupsCount++;
		}
		var socialGroupsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, socialGroupsBefore);
		writer.WriteInt16((short)socialGroupsCount);
		writer.Seek(SeekOrigin.Begin, socialGroupsAfter);
		writer.WriteInt8(PlayerState);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		Self = BooleanByteWrapper.GetFlag(flag, 0);
		Verbose = BooleanByteWrapper.GetFlag(flag, 1);
		Position = reader.ReadInt8();
		AccountNickname = reader.ReadUtfPrefixedLength16();
		AccountId = reader.ReadInt32();
		PlayerName = reader.ReadUtfPrefixedLength16();
		PlayerId = reader.ReadVarUInt32();
		AreaId = reader.ReadInt16();
		var socialGroupsCount = reader.ReadInt16();
		var socialGroups = new AbstractSocialGroupInfos[socialGroupsCount];
		for (var i = 0; i < socialGroupsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<AbstractSocialGroupInfos>(reader.ReadUInt16());
			entry.Deserialize(reader);
			socialGroups[i] = entry;
		}
		SocialGroups = socialGroups;
		PlayerState = reader.ReadInt8();
	}
}

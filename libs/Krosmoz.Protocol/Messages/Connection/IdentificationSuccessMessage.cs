// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection;

public class IdentificationSuccessMessage : DofusMessage
{
	public new const uint StaticProtocolId = 22;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IdentificationSuccessMessage Empty =>
		new() { HasRights = false, WasAlreadyConnected = false, Login = string.Empty, Nickname = string.Empty, AccountId = 0, CommunityId = 0, SecretQuestion = string.Empty, AccountCreation = 0, SubscriptionElapsedDuration = 0, SubscriptionEndDate = 0 };

	public required bool HasRights { get; set; }

	public required bool WasAlreadyConnected { get; set; }

	public required string Login { get; set; }

	public required string Nickname { get; set; }

	public required int AccountId { get; set; }

	public required sbyte CommunityId { get; set; }

	public required string SecretQuestion { get; set; }

	public required double AccountCreation { get; set; }

	public required double SubscriptionElapsedDuration { get; set; }

	public required double SubscriptionEndDate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, HasRights);
		flag = BooleanByteWrapper.SetFlag(flag, 1, WasAlreadyConnected);
		writer.WriteUInt8(flag);
		writer.WriteUtfPrefixedLength16(Login);
		writer.WriteUtfPrefixedLength16(Nickname);
		writer.WriteInt32(AccountId);
		writer.WriteInt8(CommunityId);
		writer.WriteUtfPrefixedLength16(SecretQuestion);
		writer.WriteDouble(AccountCreation);
		writer.WriteDouble(SubscriptionElapsedDuration);
		writer.WriteDouble(SubscriptionEndDate);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		HasRights = BooleanByteWrapper.GetFlag(flag, 0);
		WasAlreadyConnected = BooleanByteWrapper.GetFlag(flag, 1);
		Login = reader.ReadUtfPrefixedLength16();
		Nickname = reader.ReadUtfPrefixedLength16();
		AccountId = reader.ReadInt32();
		CommunityId = reader.ReadInt8();
		SecretQuestion = reader.ReadUtfPrefixedLength16();
		AccountCreation = reader.ReadDouble();
		SubscriptionElapsedDuration = reader.ReadDouble();
		SubscriptionEndDate = reader.ReadDouble();
	}
}

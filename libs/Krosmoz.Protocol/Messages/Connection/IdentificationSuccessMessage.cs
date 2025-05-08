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
		new() { HasRights = false, WasAlreadyConnected = false, Login = string.Empty, Nickname = string.Empty, AccountId = 0, CommunityId = 0, SecretQuestion = string.Empty, SubscriptionEndDate = 0, AccountCreation = 0 };

	public required bool HasRights { get; set; }

	public required bool WasAlreadyConnected { get; set; }

	public required string Login { get; set; }

	public required string Nickname { get; set; }

	public required int AccountId { get; set; }

	public required sbyte CommunityId { get; set; }

	public required string SecretQuestion { get; set; }

	public required double SubscriptionEndDate { get; set; }

	public required double AccountCreation { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		BooleanByteWrapper.SetFlag(flag, 0, HasRights);
		BooleanByteWrapper.SetFlag(flag, 1, WasAlreadyConnected);
		writer.WriteByte(flag);
		writer.WriteUtfLengthPrefixed16(Login);
		writer.WriteUtfLengthPrefixed16(Nickname);
		writer.WriteInt(AccountId);
		writer.WriteSByte(CommunityId);
		writer.WriteUtfLengthPrefixed16(SecretQuestion);
		writer.WriteDouble(SubscriptionEndDate);
		writer.WriteDouble(AccountCreation);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadByte();
		HasRights = BooleanByteWrapper.GetFlag(flag, 0);
		WasAlreadyConnected = BooleanByteWrapper.GetFlag(flag, 1);
		Login = reader.ReadUtfLengthPrefixed16();
		Nickname = reader.ReadUtfLengthPrefixed16();
		AccountId = reader.ReadInt();
		CommunityId = reader.ReadSByte();
		SecretQuestion = reader.ReadUtfLengthPrefixed16();
		SubscriptionEndDate = reader.ReadDouble();
		AccountCreation = reader.ReadDouble();
	}
}

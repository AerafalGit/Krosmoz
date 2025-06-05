// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection;

public sealed class IdentificationSuccessWithLoginTokenMessage : IdentificationSuccessMessage
{
	public new const uint StaticProtocolId = 6209;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new IdentificationSuccessWithLoginTokenMessage Empty =>
		new() { SubscriptionEndDate = 0, SubscriptionElapsedDuration = 0, AccountCreation = 0, SecretQuestion = string.Empty, CommunityId = 0, AccountId = 0, Nickname = string.Empty, Login = string.Empty, WasAlreadyConnected = false, HasRights = false, LoginToken = string.Empty };

	public required string LoginToken { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(LoginToken);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		LoginToken = reader.ReadUtfPrefixedLength16();
	}
}

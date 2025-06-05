// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Friend;

public class AbstractContactInformations : DofusType
{
	public new const ushort StaticProtocolId = 380;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AbstractContactInformations Empty =>
		new() { AccountId = 0, AccountName = string.Empty };

	public required int AccountId { get; set; }

	public required string AccountName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(AccountId);
		writer.WriteUtfPrefixedLength16(AccountName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AccountId = reader.ReadInt32();
		AccountName = reader.ReadUtfPrefixedLength16();
	}
}

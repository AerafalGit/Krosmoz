// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Social;

public class ContactLookRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5932;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ContactLookRequestMessage Empty =>
		new() { RequestId = 0, ContactType = 0 };

	public required byte RequestId { get; set; }

	public required sbyte ContactType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt8(RequestId);
		writer.WriteInt8(ContactType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RequestId = reader.ReadUInt8();
		ContactType = reader.ReadInt8();
	}
}

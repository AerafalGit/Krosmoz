// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Social;

public sealed class ContactLookRequestByIdMessage : ContactLookRequestMessage
{
	public new const uint StaticProtocolId = 5935;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ContactLookRequestByIdMessage Empty =>
		new() { ContactType = 0, RequestId = 0, PlayerId = 0 };

	public required uint PlayerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(PlayerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PlayerId = reader.ReadVarUInt32();
	}
}

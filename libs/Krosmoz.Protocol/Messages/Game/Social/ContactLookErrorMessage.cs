// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Social;

public sealed class ContactLookErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6045;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ContactLookErrorMessage Empty =>
		new() { RequestId = 0 };

	public required uint RequestId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(RequestId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RequestId = reader.ReadVarUInt32();
	}
}

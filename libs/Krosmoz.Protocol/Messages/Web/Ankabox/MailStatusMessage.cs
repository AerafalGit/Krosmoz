// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Web.Ankabox;

public class MailStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6275;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MailStatusMessage Empty =>
		new() { Unread = 0, Total = 0 };

	public required ushort Unread { get; set; }

	public required ushort Total { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Unread);
		writer.WriteVarUInt16(Total);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Unread = reader.ReadVarUInt16();
		Total = reader.ReadVarUInt16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Report;

public sealed class CharacterReportMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6079;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterReportMessage Empty =>
		new() { ReportedId = 0, Reason = 0 };

	public required uint ReportedId { get; set; }

	public required sbyte Reason { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ReportedId);
		writer.WriteInt8(Reason);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ReportedId = reader.ReadVarUInt32();
		Reason = reader.ReadInt8();
	}
}

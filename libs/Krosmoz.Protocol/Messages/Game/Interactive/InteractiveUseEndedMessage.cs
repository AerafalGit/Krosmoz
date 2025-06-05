// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive;

public sealed class InteractiveUseEndedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6112;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InteractiveUseEndedMessage Empty =>
		new() { ElemId = 0, SkillId = 0 };

	public required uint ElemId { get; set; }

	public required ushort SkillId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ElemId);
		writer.WriteVarUInt16(SkillId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ElemId = reader.ReadVarUInt32();
		SkillId = reader.ReadVarUInt16();
	}
}

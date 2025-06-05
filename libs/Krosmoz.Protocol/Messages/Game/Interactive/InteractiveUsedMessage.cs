// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive;

public sealed class InteractiveUsedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5745;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InteractiveUsedMessage Empty =>
		new() { EntityId = 0, ElemId = 0, SkillId = 0, Duration = 0 };

	public required uint EntityId { get; set; }

	public required uint ElemId { get; set; }

	public required ushort SkillId { get; set; }

	public required ushort Duration { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(EntityId);
		writer.WriteVarUInt32(ElemId);
		writer.WriteVarUInt16(SkillId);
		writer.WriteVarUInt16(Duration);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		EntityId = reader.ReadVarUInt32();
		ElemId = reader.ReadVarUInt32();
		SkillId = reader.ReadVarUInt16();
		Duration = reader.ReadVarUInt16();
	}
}

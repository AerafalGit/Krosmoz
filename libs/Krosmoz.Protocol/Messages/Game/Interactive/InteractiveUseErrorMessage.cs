// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Interactive;

public sealed class InteractiveUseErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6384;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InteractiveUseErrorMessage Empty =>
		new() { ElemId = 0, SkillInstanceUid = 0 };

	public required int ElemId { get; set; }

	public required int SkillInstanceUid { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ElemId);
		writer.WriteInt(SkillInstanceUid);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ElemId = reader.ReadInt();
		SkillInstanceUid = reader.ReadInt();
	}
}

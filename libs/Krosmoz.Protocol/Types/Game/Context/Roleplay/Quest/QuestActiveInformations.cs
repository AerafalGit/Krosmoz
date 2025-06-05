// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Quest;

public class QuestActiveInformations : DofusType
{
	public new const ushort StaticProtocolId = 381;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static QuestActiveInformations Empty =>
		new() { QuestId = 0 };

	public required ushort QuestId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(QuestId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		QuestId = reader.ReadVarUInt16();
	}
}

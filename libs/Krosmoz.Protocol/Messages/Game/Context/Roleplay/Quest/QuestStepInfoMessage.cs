// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Quest;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Quest;

public sealed class QuestStepInfoMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5625;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static QuestStepInfoMessage Empty =>
		new() { Infos = QuestActiveInformations.Empty };

	public required QuestActiveInformations Infos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Infos.ProtocolId);
		Infos.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Infos = Types.TypeFactory.CreateType<QuestActiveInformations>(reader.ReadUInt16());
		Infos.Deserialize(reader);
	}
}

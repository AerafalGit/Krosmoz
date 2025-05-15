// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Initialization;

public sealed class ServerExperienceModificatorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6237;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ServerExperienceModificatorMessage Empty =>
		new() { ExperiencePercent = 0 };

	public required short ExperiencePercent { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(ExperiencePercent);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ExperiencePercent = reader.ReadInt16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Updater;

namespace Krosmoz.Protocol.Messages.Updater.Parts;

public sealed class PartInfoMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1508;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PartInfoMessage Empty =>
		new() { Part = ContentPart.Empty, InstallationPercent = 0 };

	public required ContentPart Part { get; set; }

	public required float InstallationPercent { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Part.Serialize(writer);
		writer.WriteSingle(InstallationPercent);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Part = ContentPart.Empty;
		Part.Deserialize(reader);
		InstallationPercent = reader.ReadSingle();
	}
}

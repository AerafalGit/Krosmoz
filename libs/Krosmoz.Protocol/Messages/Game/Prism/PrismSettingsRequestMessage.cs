// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismSettingsRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6437;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismSettingsRequestMessage Empty =>
		new() { SubAreaId = 0, StartDefenseTime = 0 };

	public required short SubAreaId { get; set; }

	public required sbyte StartDefenseTime { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(SubAreaId);
		writer.WriteSByte(StartDefenseTime);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadShort();
		StartDefenseTime = reader.ReadSByte();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobAllowMultiCraftRequestSetMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5749;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static JobAllowMultiCraftRequestSetMessage Empty =>
		new() { Enabled = false };

	public required bool Enabled { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Enabled);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Enabled = reader.ReadBoolean();
	}
}

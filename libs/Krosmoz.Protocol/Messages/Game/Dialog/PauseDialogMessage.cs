// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Dialog;

public sealed class PauseDialogMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6012;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PauseDialogMessage Empty =>
		new() { DialogType = 0 };

	public required sbyte DialogType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(DialogType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DialogType = reader.ReadInt8();
	}
}

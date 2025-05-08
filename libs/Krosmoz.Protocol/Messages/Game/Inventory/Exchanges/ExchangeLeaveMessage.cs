// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Game.Dialog;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeLeaveMessage : LeaveDialogMessage
{
	public new const uint StaticProtocolId = 5628;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeLeaveMessage Empty =>
		new() { DialogType = 0, Success = false };

	public required bool Success { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(Success);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Success = reader.ReadBoolean();
	}
}

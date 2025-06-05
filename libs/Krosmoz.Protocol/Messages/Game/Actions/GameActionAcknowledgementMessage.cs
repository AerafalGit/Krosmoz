// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions;

public sealed class GameActionAcknowledgementMessage : DofusMessage
{
	public new const uint StaticProtocolId = 957;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameActionAcknowledgementMessage Empty =>
		new() { Valid = false, ActionId = 0 };

	public required bool Valid { get; set; }

	public required sbyte ActionId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Valid);
		writer.WriteInt8(ActionId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Valid = reader.ReadBoolean();
		ActionId = reader.ReadInt8();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Startup;

public sealed class StartupActionsObjetAttributionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1303;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static StartupActionsObjetAttributionMessage Empty =>
		new() { ActionId = 0, CharacterId = 0 };

	public required int ActionId { get; set; }

	public required int CharacterId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(ActionId);
		writer.WriteInt32(CharacterId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ActionId = reader.ReadInt32();
		CharacterId = reader.ReadInt32();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class TaxCollectorStateUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6455;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TaxCollectorStateUpdateMessage Empty =>
		new() { UniqueId = 0, State = 0 };

	public required int UniqueId { get; set; }

	public required sbyte State { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(UniqueId);
		writer.WriteInt8(State);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		UniqueId = reader.ReadInt32();
		State = reader.ReadInt8();
	}
}

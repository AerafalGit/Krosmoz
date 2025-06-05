// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Emote;

public sealed class EmotePlayMessage : EmotePlayAbstractMessage
{
	public new const uint StaticProtocolId = 5683;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new EmotePlayMessage Empty =>
		new() { EmoteStartTime = 0, EmoteId = 0, ActorId = 0, AccountId = 0 };

	public required int ActorId { get; set; }

	public required int AccountId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(ActorId);
		writer.WriteInt32(AccountId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ActorId = reader.ReadInt32();
		AccountId = reader.ReadInt32();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses.Guild;

public sealed class HouseGuildShareRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5704;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseGuildShareRequestMessage Empty =>
		new() { Enable = false, Rights = 0 };

	public required bool Enable { get; set; }

	public required uint Rights { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Enable);
		writer.WriteVarUInt32(Rights);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Enable = reader.ReadBoolean();
		Rights = reader.ReadVarUInt32();
	}
}

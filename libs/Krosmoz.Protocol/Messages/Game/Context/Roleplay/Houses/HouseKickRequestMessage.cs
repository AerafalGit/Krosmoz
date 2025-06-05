// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public sealed class HouseKickRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5698;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseKickRequestMessage Empty =>
		new() { Id = 0 };

	public required uint Id { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Id);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadVarUInt32();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Ui;

public sealed class ClientUIOpenedByObjectMessage : ClientUIOpenedMessage
{
	public new const uint StaticProtocolId = 6463;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ClientUIOpenedByObjectMessage Empty =>
		new() { Type = 0, Uid = 0 };

	public required uint Uid { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Uid);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Uid = reader.ReadVarUInt32();
	}
}

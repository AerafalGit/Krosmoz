// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameContextCreateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 200;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameContextCreateMessage Empty =>
		new() { Context = 0 };

	public required sbyte Context { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Context);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Context = reader.ReadInt8();
	}
}

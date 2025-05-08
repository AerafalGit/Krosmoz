// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicSetAwayModeRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5665;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicSetAwayModeRequestMessage Empty =>
		new() { Enable = false, Invisible = false };

	public required bool Enable { get; set; }

	public required bool Invisible { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		BooleanByteWrapper.SetFlag(flag, 0, Enable);
		BooleanByteWrapper.SetFlag(flag, 1, Invisible);
		writer.WriteByte(flag);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadByte();
		Enable = BooleanByteWrapper.GetFlag(flag, 0);
		Invisible = BooleanByteWrapper.GetFlag(flag, 1);
	}
}

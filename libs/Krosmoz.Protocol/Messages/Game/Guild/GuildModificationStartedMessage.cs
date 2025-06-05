// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildModificationStartedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6324;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildModificationStartedMessage Empty =>
		new() { CanChangeName = false, CanChangeEmblem = false };

	public required bool CanChangeName { get; set; }

	public required bool CanChangeEmblem { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, CanChangeName);
		flag = BooleanByteWrapper.SetFlag(flag, 1, CanChangeEmblem);
		writer.WriteUInt8(flag);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		CanChangeName = BooleanByteWrapper.GetFlag(flag, 0);
		CanChangeEmblem = BooleanByteWrapper.GetFlag(flag, 1);
	}
}

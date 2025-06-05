// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceModificationStartedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6444;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceModificationStartedMessage Empty =>
		new() { CanChangeName = false, CanChangeTag = false, CanChangeEmblem = false };

	public required bool CanChangeName { get; set; }

	public required bool CanChangeTag { get; set; }

	public required bool CanChangeEmblem { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, CanChangeName);
		flag = BooleanByteWrapper.SetFlag(flag, 1, CanChangeTag);
		flag = BooleanByteWrapper.SetFlag(flag, 2, CanChangeEmblem);
		writer.WriteUInt8(flag);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		CanChangeName = BooleanByteWrapper.GetFlag(flag, 0);
		CanChangeTag = BooleanByteWrapper.GetFlag(flag, 1);
		CanChangeEmblem = BooleanByteWrapper.GetFlag(flag, 2);
	}
}

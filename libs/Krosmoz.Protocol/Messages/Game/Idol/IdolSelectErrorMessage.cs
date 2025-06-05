// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Idol;

public sealed class IdolSelectErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6584;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IdolSelectErrorMessage Empty =>
		new() { Activate = false, Party = false, Reason = 0, IdolId = 0 };

	public required bool Activate { get; set; }

	public required bool Party { get; set; }

	public required sbyte Reason { get; set; }

	public required ushort IdolId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, Activate);
		flag = BooleanByteWrapper.SetFlag(flag, 1, Party);
		writer.WriteUInt8(flag);
		writer.WriteInt8(Reason);
		writer.WriteVarUInt16(IdolId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		Activate = BooleanByteWrapper.GetFlag(flag, 0);
		Party = BooleanByteWrapper.GetFlag(flag, 1);
		Reason = reader.ReadInt8();
		IdolId = reader.ReadVarUInt16();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Approach;

public sealed class AccountLoggingKickedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6029;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AccountLoggingKickedMessage Empty =>
		new() { Days = 0, Hours = 0, Minutes = 0 };

	public required ushort Days { get; set; }

	public required sbyte Hours { get; set; }

	public required sbyte Minutes { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Days);
		writer.WriteInt8(Hours);
		writer.WriteInt8(Minutes);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Days = reader.ReadVarUInt16();
		Hours = reader.ReadInt8();
		Minutes = reader.ReadInt8();
	}
}

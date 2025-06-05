// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class HumanOptionObjectUse : HumanOption
{
	public new const ushort StaticProtocolId = 449;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new HumanOptionObjectUse Empty =>
		new() { DelayTypeId = 0, DelayEndTime = 0, ObjectGID = 0 };

	public required sbyte DelayTypeId { get; set; }

	public required double DelayEndTime { get; set; }

	public required ushort ObjectGID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(DelayTypeId);
		writer.WriteDouble(DelayEndTime);
		writer.WriteVarUInt16(ObjectGID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		DelayTypeId = reader.ReadInt8();
		DelayEndTime = reader.ReadDouble();
		ObjectGID = reader.ReadVarUInt16();
	}
}

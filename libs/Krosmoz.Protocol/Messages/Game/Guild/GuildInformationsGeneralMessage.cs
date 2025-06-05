// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildInformationsGeneralMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5557;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildInformationsGeneralMessage Empty =>
		new() { Enabled = false, AbandonnedPaddock = false, Level = 0, ExpLevelFloor = 0, Experience = 0, ExpNextLevelFloor = 0, CreationDate = 0, NbTotalMembers = 0, NbConnectedMembers = 0 };

	public required bool Enabled { get; set; }

	public required bool AbandonnedPaddock { get; set; }

	public required byte Level { get; set; }

	public required ulong ExpLevelFloor { get; set; }

	public required ulong Experience { get; set; }

	public required ulong ExpNextLevelFloor { get; set; }

	public required int CreationDate { get; set; }

	public required ushort NbTotalMembers { get; set; }

	public required ushort NbConnectedMembers { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, Enabled);
		flag = BooleanByteWrapper.SetFlag(flag, 1, AbandonnedPaddock);
		writer.WriteUInt8(flag);
		writer.WriteUInt8(Level);
		writer.WriteVarUInt64(ExpLevelFloor);
		writer.WriteVarUInt64(Experience);
		writer.WriteVarUInt64(ExpNextLevelFloor);
		writer.WriteInt32(CreationDate);
		writer.WriteVarUInt16(NbTotalMembers);
		writer.WriteVarUInt16(NbConnectedMembers);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		Enabled = BooleanByteWrapper.GetFlag(flag, 0);
		AbandonnedPaddock = BooleanByteWrapper.GetFlag(flag, 1);
		Level = reader.ReadUInt8();
		ExpLevelFloor = reader.ReadVarUInt64();
		Experience = reader.ReadVarUInt64();
		ExpNextLevelFloor = reader.ReadVarUInt64();
		CreationDate = reader.ReadInt32();
		NbTotalMembers = reader.ReadVarUInt16();
		NbConnectedMembers = reader.ReadVarUInt16();
	}
}

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
		new() { Enabled = false, AbandonnedPaddock = false, Level = 0, ExpLevelFloor = 0, Experience = 0, ExpNextLevelFloor = 0, CreationDate = 0 };

	public required bool Enabled { get; set; }

	public required bool AbandonnedPaddock { get; set; }

	public required byte Level { get; set; }

	public required double ExpLevelFloor { get; set; }

	public required double Experience { get; set; }

	public required double ExpNextLevelFloor { get; set; }

	public required int CreationDate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, Enabled);
		flag = BooleanByteWrapper.SetFlag(flag, 1, AbandonnedPaddock);
		writer.WriteByte(flag);
		writer.WriteByte(Level);
		writer.WriteDouble(ExpLevelFloor);
		writer.WriteDouble(Experience);
		writer.WriteDouble(ExpNextLevelFloor);
		writer.WriteInt(CreationDate);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadByte();
		Enabled = BooleanByteWrapper.GetFlag(flag, 0);
		AbandonnedPaddock = BooleanByteWrapper.GetFlag(flag, 1);
		Level = reader.ReadByte();
		ExpLevelFloor = reader.ReadDouble();
		Experience = reader.ReadDouble();
		ExpNextLevelFloor = reader.ReadDouble();
		CreationDate = reader.ReadInt();
	}
}

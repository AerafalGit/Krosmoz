// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Connection;

public sealed class GameServerInformations : DofusType
{
	public new const ushort StaticProtocolId = 25;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GameServerInformations Empty =>
		new() { Id = 0, Status = 0, Completion = 0, IsSelectable = false, CharactersCount = 0, Date = 0 };

	public required ushort Id { get; set; }

	public required sbyte Status { get; set; }

	public required sbyte Completion { get; set; }

	public required bool IsSelectable { get; set; }

	public required sbyte CharactersCount { get; set; }

	public required double Date { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUShort(Id);
		writer.WriteSByte(Status);
		writer.WriteSByte(Completion);
		writer.WriteBoolean(IsSelectable);
		writer.WriteSByte(CharactersCount);
		writer.WriteDouble(Date);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadUShort();
		Status = reader.ReadSByte();
		Completion = reader.ReadSByte();
		IsSelectable = reader.ReadBoolean();
		CharactersCount = reader.ReadSByte();
		Date = reader.ReadDouble();
	}
}

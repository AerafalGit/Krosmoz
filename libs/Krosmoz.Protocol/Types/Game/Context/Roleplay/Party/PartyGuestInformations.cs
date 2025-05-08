// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Status;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

public sealed class PartyGuestInformations : DofusType
{
	public new const ushort StaticProtocolId = 374;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static PartyGuestInformations Empty =>
		new() { GuestId = 0, HostId = 0, Name = string.Empty, GuestLook = EntityLook.Empty, Breed = 0, Sex = false, Status = PlayerStatus.Empty };

	public required int GuestId { get; set; }

	public required int HostId { get; set; }

	public required string Name { get; set; }

	public required EntityLook GuestLook { get; set; }

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public required PlayerStatus Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(GuestId);
		writer.WriteInt(HostId);
		writer.WriteUtfLengthPrefixed16(Name);
		GuestLook.Serialize(writer);
		writer.WriteSByte(Breed);
		writer.WriteBoolean(Sex);
		writer.WriteUShort(Status.ProtocolId);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuestId = reader.ReadInt();
		HostId = reader.ReadInt();
		Name = reader.ReadUtfLengthPrefixed16();
		GuestLook = EntityLook.Empty;
		GuestLook.Deserialize(reader);
		Breed = reader.ReadSByte();
		Sex = reader.ReadBoolean();
		Status = Types.TypeFactory.CreateType<PlayerStatus>(reader.ReadUShort());
		Status.Deserialize(reader);
	}
}

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
		writer.WriteInt32(GuestId);
		writer.WriteInt32(HostId);
		writer.WriteUtfPrefixedLength16(Name);
		GuestLook.Serialize(writer);
		writer.WriteInt8(Breed);
		writer.WriteBoolean(Sex);
		writer.WriteUInt16(Status.ProtocolId);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuestId = reader.ReadInt32();
		HostId = reader.ReadInt32();
		Name = reader.ReadUtfPrefixedLength16();
		GuestLook = EntityLook.Empty;
		GuestLook.Deserialize(reader);
		Breed = reader.ReadInt8();
		Sex = reader.ReadBoolean();
		Status = TypeFactory.CreateType<PlayerStatus>(reader.ReadUInt16());
		Status.Deserialize(reader);
	}
}

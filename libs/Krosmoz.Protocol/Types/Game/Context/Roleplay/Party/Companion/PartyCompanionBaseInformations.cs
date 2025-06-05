// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party.Companion;

public class PartyCompanionBaseInformations : DofusType
{
	public new const ushort StaticProtocolId = 453;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static PartyCompanionBaseInformations Empty =>
		new() { IndexId = 0, CompanionGenericId = 0, EntityLook = EntityLook.Empty };

	public required sbyte IndexId { get; set; }

	public required sbyte CompanionGenericId { get; set; }

	public required EntityLook EntityLook { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(IndexId);
		writer.WriteInt8(CompanionGenericId);
		EntityLook.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		IndexId = reader.ReadInt8();
		CompanionGenericId = reader.ReadInt8();
		EntityLook = EntityLook.Empty;
		EntityLook.Deserialize(reader);
	}
}

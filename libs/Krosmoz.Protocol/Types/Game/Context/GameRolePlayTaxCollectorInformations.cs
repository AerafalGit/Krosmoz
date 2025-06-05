// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context;

public sealed class GameRolePlayTaxCollectorInformations : GameRolePlayActorInformations
{
	public new const ushort StaticProtocolId = 148;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayTaxCollectorInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Identification = TaxCollectorStaticInformations.Empty, GuildLevel = 0, TaxCollectorAttack = 0 };

	public required TaxCollectorStaticInformations Identification { get; set; }

	public required byte GuildLevel { get; set; }

	public required int TaxCollectorAttack { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt16(Identification.ProtocolId);
		Identification.Serialize(writer);
		writer.WriteUInt8(GuildLevel);
		writer.WriteInt32(TaxCollectorAttack);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Identification = Types.TypeFactory.CreateType<TaxCollectorStaticInformations>(reader.ReadUInt16());
		Identification.Deserialize(reader);
		GuildLevel = reader.ReadUInt8();
		TaxCollectorAttack = reader.ReadInt32();
	}
}

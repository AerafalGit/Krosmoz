// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public class GameRolePlayHumanoidInformations : GameRolePlayNamedActorInformations
{
	public new const ushort StaticProtocolId = 159;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayHumanoidInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Name = string.Empty, HumanoidInfo = HumanInformations.Empty, AccountId = 0 };

	public required HumanInformations HumanoidInfo { get; set; }

	public required int AccountId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt16(HumanoidInfo.ProtocolId);
		HumanoidInfo.Serialize(writer);
		writer.WriteInt32(AccountId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		HumanoidInfo = Types.TypeFactory.CreateType<HumanInformations>(reader.ReadUInt16());
		HumanoidInfo.Deserialize(reader);
		AccountId = reader.ReadInt32();
	}
}

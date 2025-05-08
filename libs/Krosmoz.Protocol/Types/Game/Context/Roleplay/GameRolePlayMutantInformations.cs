// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GameRolePlayMutantInformations : GameRolePlayHumanoidInformations
{
	public new const ushort StaticProtocolId = 3;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayMutantInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Name = string.Empty, AccountId = 0, HumanoidInfo = HumanInformations.Empty, MonsterId = 0, PowerLevel = 0 };

	public required int MonsterId { get; set; }

	public required sbyte PowerLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(MonsterId);
		writer.WriteSByte(PowerLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MonsterId = reader.ReadInt();
		PowerLevel = reader.ReadSByte();
	}
}

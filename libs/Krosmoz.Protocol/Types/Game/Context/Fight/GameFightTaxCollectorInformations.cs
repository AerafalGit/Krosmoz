// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightTaxCollectorInformations : GameFightAIInformations
{
	public new const ushort StaticProtocolId = 48;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightTaxCollectorInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, PreviousPositions = [], Stats = GameFightMinimalStats.Empty, Alive = false, Wave = 0, TeamId = 0, FirstNameId = 0, LastNameId = 0, Level = 0 };

	public required ushort FirstNameId { get; set; }

	public required ushort LastNameId { get; set; }

	public required byte Level { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(FirstNameId);
		writer.WriteVarUInt16(LastNameId);
		writer.WriteUInt8(Level);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		FirstNameId = reader.ReadVarUInt16();
		LastNameId = reader.ReadVarUInt16();
		Level = reader.ReadUInt8();
	}
}

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
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Stats = GameFightMinimalStats.Empty, Alive = false, TeamId = 0, FirstNameId = 0, LastNameId = 0, Level = 0 };

	public required short FirstNameId { get; set; }

	public required short LastNameId { get; set; }

	public required short Level { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(FirstNameId);
		writer.WriteShort(LastNameId);
		writer.WriteShort(Level);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		FirstNameId = reader.ReadShort();
		LastNameId = reader.ReadShort();
		Level = reader.ReadShort();
	}
}

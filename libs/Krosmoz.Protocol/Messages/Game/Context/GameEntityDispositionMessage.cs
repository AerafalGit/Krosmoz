// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameEntityDispositionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5693;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameEntityDispositionMessage Empty =>
		new() { Disposition = IdentifiedEntityDispositionInformations.Empty };

	public required IdentifiedEntityDispositionInformations Disposition { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Disposition.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Disposition = IdentifiedEntityDispositionInformations.Empty;
		Disposition.Deserialize(reader);
	}
}

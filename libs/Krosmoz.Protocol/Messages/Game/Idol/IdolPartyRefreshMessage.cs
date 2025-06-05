// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Idol;

namespace Krosmoz.Protocol.Messages.Game.Idol;

public sealed class IdolPartyRefreshMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6583;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IdolPartyRefreshMessage Empty =>
		new() { PartyIdol = PartyIdol.Empty };

	public required PartyIdol PartyIdol { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		PartyIdol.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PartyIdol = PartyIdol.Empty;
		PartyIdol.Deserialize(reader);
	}
}

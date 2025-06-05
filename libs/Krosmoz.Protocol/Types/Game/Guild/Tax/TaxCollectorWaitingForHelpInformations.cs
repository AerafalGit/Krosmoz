// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Fight;

namespace Krosmoz.Protocol.Types.Game.Guild.Tax;

public sealed class TaxCollectorWaitingForHelpInformations : TaxCollectorComplementaryInformations
{
	public new const ushort StaticProtocolId = 447;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new TaxCollectorWaitingForHelpInformations Empty =>
		new() { WaitingForHelpInfo = ProtectedEntityWaitingForHelpInfo.Empty };

	public required ProtectedEntityWaitingForHelpInfo WaitingForHelpInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		WaitingForHelpInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		WaitingForHelpInfo = ProtectedEntityWaitingForHelpInfo.Empty;
		WaitingForHelpInfo.Deserialize(reader);
	}
}

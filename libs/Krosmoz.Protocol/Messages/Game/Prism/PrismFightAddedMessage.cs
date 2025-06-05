// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Prism;

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismFightAddedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6452;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismFightAddedMessage Empty =>
		new() { Fight = PrismFightersInformation.Empty };

	public required PrismFightersInformation Fight { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Fight.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Fight = PrismFightersInformation.Empty;
		Fight.Deserialize(reader);
	}
}

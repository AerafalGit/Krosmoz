// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Types.Game.Guild.Tax;

public sealed class TaxCollectorGuildInformations : TaxCollectorComplementaryInformations
{
	public new const ushort StaticProtocolId = 446;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new TaxCollectorGuildInformations Empty =>
		new() { Guild = BasicGuildInformations.Empty };

	public required BasicGuildInformations Guild { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Guild.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Guild = BasicGuildInformations.Empty;
		Guild.Deserialize(reader);
	}
}

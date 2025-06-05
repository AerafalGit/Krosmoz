// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Guild.Tax;

public sealed class AdditionalTaxCollectorInformations : DofusType
{
	public new const ushort StaticProtocolId = 165;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AdditionalTaxCollectorInformations Empty =>
		new() { CollectorCallerName = string.Empty, Date = 0 };

	public required string CollectorCallerName { get; set; }

	public required int Date { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(CollectorCallerName);
		writer.WriteInt32(Date);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CollectorCallerName = reader.ReadUtfPrefixedLength16();
		Date = reader.ReadInt32();
	}
}

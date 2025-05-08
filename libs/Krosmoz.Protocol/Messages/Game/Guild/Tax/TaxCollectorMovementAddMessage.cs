// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild.Tax;

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class TaxCollectorMovementAddMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5917;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TaxCollectorMovementAddMessage Empty =>
		new() { Informations = TaxCollectorInformations.Empty };

	public required TaxCollectorInformations Informations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUShort(Informations.ProtocolId);
		Informations.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Informations = Types.TypeFactory.CreateType<TaxCollectorInformations>(reader.ReadUShort());
		Informations.Deserialize(reader);
	}
}

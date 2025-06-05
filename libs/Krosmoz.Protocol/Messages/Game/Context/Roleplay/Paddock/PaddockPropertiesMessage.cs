// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Paddock;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Paddock;

public sealed class PaddockPropertiesMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5824;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PaddockPropertiesMessage Empty =>
		new() { Properties = PaddockInformations.Empty };

	public required PaddockInformations Properties { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Properties.ProtocolId);
		Properties.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Properties = Types.TypeFactory.CreateType<PaddockInformations>(reader.ReadUInt16());
		Properties.Deserialize(reader);
	}
}

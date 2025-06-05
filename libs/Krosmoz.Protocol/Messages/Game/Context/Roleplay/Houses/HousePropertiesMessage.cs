// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.House;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public sealed class HousePropertiesMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5734;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HousePropertiesMessage Empty =>
		new() { Properties = HouseInformations.Empty };

	public required HouseInformations Properties { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Properties.ProtocolId);
		Properties.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Properties = Types.TypeFactory.CreateType<HouseInformations>(reader.ReadUInt16());
		Properties.Deserialize(reader);
	}
}

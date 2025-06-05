// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class HumanOptionAlliance : HumanOption
{
	public new const ushort StaticProtocolId = 425;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new HumanOptionAlliance Empty =>
		new() { AllianceInformations = AllianceInformations.Empty, Aggressable = 0 };

	public required AllianceInformations AllianceInformations { get; set; }

	public required sbyte Aggressable { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		AllianceInformations.Serialize(writer);
		writer.WriteInt8(Aggressable);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AllianceInformations = AllianceInformations.Empty;
		AllianceInformations.Deserialize(reader);
		Aggressable = reader.ReadInt8();
	}
}

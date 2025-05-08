// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Paddock;

public class PaddockInformations : DofusType
{
	public new const ushort StaticProtocolId = 132;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static PaddockInformations Empty =>
		new() { MaxOutdoorMount = 0, MaxItems = 0 };

	public required short MaxOutdoorMount { get; set; }

	public required short MaxItems { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(MaxOutdoorMount);
		writer.WriteShort(MaxItems);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MaxOutdoorMount = reader.ReadShort();
		MaxItems = reader.ReadShort();
	}
}

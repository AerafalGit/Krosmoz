// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Paddock;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildPaddockBoughtMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5952;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildPaddockBoughtMessage Empty =>
		new() { PaddockInfo = PaddockContentInformations.Empty };

	public required PaddockContentInformations PaddockInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		PaddockInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PaddockInfo = PaddockContentInformations.Empty;
		PaddockInfo.Deserialize(reader);
	}
}

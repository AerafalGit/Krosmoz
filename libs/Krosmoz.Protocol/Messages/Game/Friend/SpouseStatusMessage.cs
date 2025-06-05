// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class SpouseStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6265;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SpouseStatusMessage Empty =>
		new() { HasSpouse = false };

	public required bool HasSpouse { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(HasSpouse);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HasSpouse = reader.ReadBoolean();
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Friend;

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class SpouseInformationsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6356;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SpouseInformationsMessage Empty =>
		new() { Spouse = FriendSpouseInformations.Empty };

	public required FriendSpouseInformations Spouse { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Spouse.ProtocolId);
		Spouse.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Spouse = Types.TypeFactory.CreateType<FriendSpouseInformations>(reader.ReadUInt16());
		Spouse.Deserialize(reader);
	}
}

// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Friend;

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class IgnoredAddedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5678;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IgnoredAddedMessage Empty =>
		new() { IgnoreAdded = IgnoredInformations.Empty, Session = false };

	public required IgnoredInformations IgnoreAdded { get; set; }

	public required bool Session { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(IgnoreAdded.ProtocolId);
		IgnoreAdded.Serialize(writer);
		writer.WriteBoolean(Session);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		IgnoreAdded = Types.TypeFactory.CreateType<IgnoredInformations>(reader.ReadUInt16());
		IgnoreAdded.Deserialize(reader);
		Session = reader.ReadBoolean();
	}
}

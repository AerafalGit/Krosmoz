// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharacterSelectedErrorMissingMapPackMessage : CharacterSelectedErrorMessage
{
	public new const uint StaticProtocolId = 6300;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharacterSelectedErrorMissingMapPackMessage Empty =>
		new() { SubAreaId = 0 };

	public required int SubAreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SubAreaId = reader.ReadInt();
	}
}

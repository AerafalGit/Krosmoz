// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Choice;

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharacterSelectedSuccessMessage : DofusMessage
{
	public new const uint StaticProtocolId = 153;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterSelectedSuccessMessage Empty =>
		new() { Infos = CharacterBaseInformations.Empty, IsCollectingStats = false };

	public required CharacterBaseInformations Infos { get; set; }

	public required bool IsCollectingStats { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Infos.Serialize(writer);
		writer.WriteBoolean(IsCollectingStats);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Infos = CharacterBaseInformations.Empty;
		Infos.Deserialize(reader);
		IsCollectingStats = reader.ReadBoolean();
	}
}
